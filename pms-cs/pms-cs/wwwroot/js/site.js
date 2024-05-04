// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const addButton = document.querySelector('.add');
const popUp = document.querySelector(".style__popup");

addButton.addEventListener('click', function(e) {
    e._isClick = true;
    popUp.classList.add('active');
});

document.body.addEventListener('click', function(e) {
    if (e._isClick == true) return
    popUp.classList.remove("active");
});

const btnCopy = document.querySelector(".btnCopy");
const copyTextSuccess = document.querySelector(".copying");
btnCopy.addEventListener("click", (e) => {
    e.preventDefault();
    let copyText = document.querySelector(".referralLinkText");
   
    copyText.select();
    navigator.clipboard.writeText(copyText.value);
    copyText.blur();
    copyTextSuccess.style.display = "inline-block";
});
