using System;
using System.Collections.Generic ;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using company.Models;
using System.Net.Mail;
using System.Net;
using System.Data.Entity.Validation;
using System.Web.Security;
namespace company.Controllers
{
    
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        tshirtsEntities db = new tshirtsEntities();
        List<string> orderList = new List<string>(); 
       
        [HttpGet]
        public ActionResult Index(string Search ,string email)
        {
            
       ColorProduct c = new ColorProduct();
       //     var productid=(from p in db.Product where p.ProductName.StartsWith(Search)
       //                          select p.ProductId
       //                          ).ToList();

       //      HashSet<int> diffids = new HashSet<int>(productid);
       //         //You will have the difference here
       //         var results = db.Color.Where(m => diffids.Contains(m.ProductId)).ToList();
       ////ViewData["orders"] = Session["orders"];
               
                
                    return View(db.Product.Where(x=>x.ProductName.StartsWith(Search)||Search==null).ToList());
                
        }
        

        [HttpPost]
        public ActionResult Index(string email)
        {



            return RedirectToAction("tshirtorder", "Home", new { email = email });
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public ActionResult changing()
        {
            return View();
        }
        [HttpGet]
        public ActionResult changepassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changepassword(String mail ,string Password)
        {

            Users user = new Users();
            bool status = false;
            var row = (from u in db.Users
                               where u.Email == mail
                               select u).FirstOrDefault();

            user=(from u in db.Users
                               where u.Email == mail
                               select u).FirstOrDefault();
            user.code = "null";
          
            user.Password = Crypto.Hash(Password);
            user.ConfirmPassword = Crypto.Hash(Password);
           
           
          
            db.SaveChanges();
            status = true;
            ViewBag.status = status;

            return RedirectToAction("changing","Home");
        }
     
        [HttpGet]
        public ActionResult matchcode(string mail)
        {
            ViewBag.mail = mail;
            
            return View(db.Users.ToList());
        }
        [HttpPost]
        public ActionResult matchcode(String code,string mail)
        {
            bool status = false;
            var x = Request.Url.PathAndQuery;
            string cod = (from u in db.Users
                          where u.Email == mail
                          select u.code).FirstOrDefault();
            if (cod == code)
            {
                return RedirectToAction("changepassword", "Home", new { mail = mail });

            }
            else {
                status = true;
                ViewBag.status = status;
                return RedirectToAction("matchcode", "Home", new { mail = mail });
            }
            
        }
        [HttpGet]
        public ActionResult forgetpassword()
        {
            return View(db.Users.ToList());
        }
       
        [HttpPost]
        public ActionResult forgetpassword(string Email)
        {
            Users use= new Users();
            bool status = false;
            
           
        string code=  sendemailforgetpass(Email);
        use = (from u in db.Users
               where u.Email == Email
               select u).FirstOrDefault();
                      
       use.code = code;
       use.ConfirmPassword = use.Password;
           db.SaveChanges();
            status = true;
            ViewBag.status = status;
            return RedirectToAction("matchcode", "Home", new { mail= Email});
        }
        [HttpGet]
        public ActionResult tshirtorder(int id1, string email)
        
        {
            tshirtsEntities db = new tshirtsEntities();


            return View(db.Color.ToList());
        }
        
        [HttpPost]
        public ActionResult tshirtorder(string email, int cid, Order ord)
        {
            tshirtsEntities db = new tshirtsEntities();
            OrderDetails orderdetails = new OrderDetails();
            //if (ModelState.IsValid)
            //{
            //    int userid = (from c in (db.Users)
            //                    where c.Email == email
            //                    select c.UserId).FirstOrDefault();
            //  int productid =  (from c in (db.Color)
            //                    where c.ColorId == cid
            //                    select c.ProductId).FirstOrDefault();
            //    ord.Quentity = Convert.ToInt32(ord.Quentity);
            //    ord.OrderDate = DateTime.Now;

            //    ord.ColorId = cid;

            //    ord.UserId = userid;


            //    db.Order.Add(ord);
            //    db.SaveChanges();
            //     orderdetails.ProductId = productid;
            //     orderdetails.OrderId = ord.OrderId;
            //     db.OrderDetails.Add(orderdetails);
            //     db.SaveChanges();
            //    return View();
            //}


            return View();
        }
       
        public PartialViewResult showorder()
        {
            
            tshirtsEntities db = new tshirtsEntities();
           
            return PartialView("Partial1", db.PreviousOrder.ToList());

        }
        [HttpGet]
        public ActionResult yourorders(string email)
        {

            //if (email == null)
            //{

            //    return RedirectToAction("login", "Home");
            //}

            //else
            //{

                return View(db.OrderDetails.ToList());
            
        }
       
        public ActionResult buy( string email ,string adress ,int phone1,int phone2)
        {
            
            Session["orders"] = db.PreviousOrder;
            Order ord=new Order();
            OrderDetails orddetails = new OrderDetails();
           
            ord.UserId = (from o in db.Users
                          where o.Email == email
                          select o.UserId).FirstOrDefault();
            ord.OrderDate = DateTime.Now;
            db.Order.Add(ord);
            db.SaveChanges();
            sendemailorder(adress, phone1, phone2, ord.UserId);
            foreach (var x in db.PreviousOrder)
            {
                if (x.userid == ord.UserId)
                {
                    orddetails.Size = x.Size;
                    orddetails.Quentity = x.Quentity;
                    orddetails.ProductId = x.productid;

                    orddetails.ColorId = Convert.ToInt32((from o in db.Color
                                                          where o.color1 == x.color && o.ProductId == x.productid
                                                          select o.ColorId).FirstOrDefault());
                    orddetails.OrderId = ord.OrderId;
                    db.OrderDetails.Add(orddetails);

                    db.PreviousOrder.Remove(x);
                    db.SaveChanges();
                    Session["status"]  = true;
                }
            }
          



            

           return RedirectToAction("index", "Home", new {  email=email });
        }
       
        [HttpPost]
        public void removerow(int id)
        {
            var row = db.PreviousOrder.Find((id));
            db.PreviousOrder.Remove(row);
            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult addtocart(int Quentity, string Size, string colorid, int productid, string email)
        {

            if (email == "")
            {
                return RedirectToAction("login", "Home");
            }

            else
            {
                tshirtsEntities db = new tshirtsEntities();
                PreviousOrder preord = new PreviousOrder();
                preord.color = colorid;
                preord.productid = productid;
                preord.Quentity = Quentity;
                preord.Size = Size;
                db.PreviousOrder.Add(preord);

                preord.userid = Convert.ToInt16((from o in db.Users
                                                 where o.Email == email
                                                 select o.UserId).FirstOrDefault());
                //orderList.Add(Quentity.ToString());
                //orderList.Add(Size);
                //orderList.Add(colorid.ToString());
                db.SaveChanges();
                Session["orders"] = db.PreviousOrder;

                return RedirectToAction("tshirtorder", "Home", new { email = email, id1 = productid });
            }
        }

    
        [HttpGet]
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult signup([Bind(Exclude= "IsEmailVerified,ActivationCode")] Users user)
        {
           string Message="";
            bool Status=false;
            //email is exist
            if (ModelState.IsValid)
            {
                var emailexist = emailisexist(user.Email);
                if (emailexist)
                {
                    ModelState.AddModelError("emailexist", " email is already exist");
                    return View(user);
                }

                // generate activationcode
                user.ActivationCode = Guid.NewGuid();


                // convert pass to increption
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                user.IsEmailVerified = false;
               
                 //save data to data base
                using(tshirtsEntities db=new tshirtsEntities()){
                    db.Users.Add(user);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                
                }
                // send verification  email to user
                sendemailverificationlink(user.Email, user.ActivationCode.ToString());
                Message="registration successfully done account activation link has been send to email id" + user.Email;
                 Status=true;
                
            }
             else
            {

                Message = "invalid request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;



            return View();
        }

        [HttpGet]
        public ActionResult verifyaccount(string id)
        {
            bool Status =false;
            using ( tshirtsEntities dc= new tshirtsEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else {

                    ViewBag.Message = "invalid request";
                }
            }

            ViewBag.Status = Status;
            return View();
        
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(userlogin login, string returnurl, string email)
        {
            
            string Message = "";
            using (tshirtsEntities dc = new tshirtsEntities())
            {
                var v = dc.Users.Where(a => a.Email == login.EMail).FirstOrDefault();

                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        if (login.EMail == "hassan.ashrafAcc@gmail.com")

                        {
                            return RedirectToAction("index", "Default1", new {  Area="Amincompany"});
                        
                        }


                        int timeout = login.remeberme ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.EMail, login.remeberme, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookies.Expires = DateTime.Now.AddMinutes(timeout);
                        cookies.HttpOnly = true;
                        Response.Cookies.Add(cookies);


                    }
                    else
                    {
                        Message = "invalid creditional porvided";
                    }
                    if (Url.IsLocalUrl(returnurl))
                    {

                        return Redirect(returnurl);
                    }
                    else
                    {

                        return RedirectToAction("index", "Home", new { email=email });
                    }

                }

                else
                {

                    Message = "invalid creditional porvided";
                }

            }
            return View() ;
        }
        //log out
        [Authorize]
        [HttpPost]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Home");

        }


        [NonAction]
        public void sendemailverificationlink(string email, string activationcode)
        {
            var verifyurl = "/Home/verifyaccount/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);
            var fromemail = new MailAddress("reemyasserfcis@gmail.com","H&R");
            var toemail = new MailAddress(email);
            var fromemailpass = "rimo roma0171451911";
            string subject = "your account is successfully create";
            string body = "<br/> <br/> we are excited to tell you that your H&R account is" +
            " successfully created . please click on the below link to verify your account" +
            "<br/> <br/> <a href ='" + link + "' >" + link + "</a> ";
          
            var smtp = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Timeout = 10000,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
               
                Credentials = new NetworkCredential(fromemail.Address, fromemailpass)
            };
            using (var message = new MailMessage(fromemail, toemail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);


        
        
        
        
        
        
        }

        public void sendemailorder( string adress, int phone1 ,int phone2 ,int userid )
        {
           
            var fromemail = new MailAddress("reemyasserfcis@gmail.com", "H&R");
            var toemail = new MailAddress("reemyasserfcis@gmail.com");
            var fromemailpass = "rimo roma0171451911";
            var order= (from o in db.PreviousOrder where o.userid==userid
                       select o).ToList();
            string ord="" ,ford="";
               foreach(var x in order){
                   ord = "<br/><br/>product name  = " + (from o in db.Product
                                                         where o.ProductId == x.productid
                       select o.ProductName).FirstOrDefault()+
                       "<br/>size  = " + x.Size +
                      "<br/>Quentity  = " + x.Quentity +
                "<br/>color  = " + x.color +
                         "<br/> buyer'sname  = " + (from o in db.Users
                                                         where o.UserId == x.userid
                       select o.FirstName).FirstOrDefault()+" "+(from o in db.Users where o.UserId==x.userid
                       select o.LastName).FirstOrDefault();

                   ord = ord + ford;
                   ford = ord;
            
            } 
            string subject = "there is order";
            string body = "<br/> <br/> there is order <br/> the adress =  " + adress + "<br/> the phone = " + phone1 +
            "<br/> the anther phone = " + phone2 +
            "<br/> his order " + ord;
         
            var smtp = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Timeout = 10000,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,

                Credentials = new NetworkCredential(fromemail.Address, fromemailpass)
            };
            using (var message = new MailMessage(fromemail, toemail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);








        }

        public string sendemailforgetpass(string email)
        {

            var fromemail = new MailAddress("reemyasserfcis@gmail.com", "H&R");
            var toemail = new MailAddress(email);
            var fromemailpass = "rimo roma0171451911";
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            string subject = "  change your password";
            string body = "your code  is "+ r ;

            var smtp = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Timeout = 500000,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
              
                Credentials = new NetworkCredential(fromemail.Address, fromemailpass)
            };
            using (var message = new MailMessage(fromemail, toemail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);





            return r;


        }




        [NonAction]
        public bool emailisexist(string email)
        { 
        using(tshirtsEntities db=new tshirtsEntities()){

           var v = db.Users.Where(x => x.Email == email).FirstOrDefault();
           return v != null;
        }
        
        
        
        }

	}
}