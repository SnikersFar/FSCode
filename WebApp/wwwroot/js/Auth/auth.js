//Forgot password

document.addEventListener("DOMContentLoaded", function () {
    const logInButton = document.querySelector(".authLogIn");
    const forgotPasswordBlock = document.querySelector(".forgotpasswordBlock");

    // Задержка в 15 секунд перед появлением кнопки "Forgot password"
    setTimeout(function () {
        forgotPasswordBlock.style.maxHeight = forgotPasswordBlock.scrollHeight + "px";
    }, 15000); // 15000 миллисекунд = 15 секунд

    logInButton.addEventListener("click", function () {
        if (forgotPasswordBlock.style.maxHeight) {
            // Если высота уже задана, то скрыть блок "Forgot password"
            forgotPasswordBlock.style.maxHeight = null;
        } else {
            // Если высота не задана, то плавно изменить высоту блока "Forgot password", чтобы он выплыл
            forgotPasswordBlock.style.maxHeight = forgotPasswordBlock.scrollHeight + "px";
        }
    });
});