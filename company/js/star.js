

const sta = document.querySelector(".stars").children;

const ratingValue = document.querySelector("#starvalue");
let index;

for (let i = 0; i < sta.length; i++) {
    sta[i].addEventListener("mouseover", function () {
        
        for (let j = 0; j < sta.length; j++) {
            sta[j].classList.remove("fa-star");
            sta[j].classList.add("fa","fa-star-o");
        }
        for (let j = 0; j <= i; j++) {
            sta[j].classList.remove("fa","fa-star-o");
            sta[j].classList.add("fa-star");
        }
    })
    sta[i].addEventListener("click", function () {
        ratingValue.value = i + 1;
        index = i;
    })
    sta[i].addEventListener("mouseout", function () {

        for (let j = 0; j < sta.length; j++) {
            sta[j].classList.remove("fa-star");
            sta[j].classList.add("fa","fa-star-o");
        }
        for (let j = 0; j <= index; j++) {
            sta[j].classList.remove("fa","fa-star-o");
            sta[j].classList.add("fa-star");
        }
    })
}






