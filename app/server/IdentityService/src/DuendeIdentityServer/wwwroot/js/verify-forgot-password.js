document.addEventListener("DOMContentLoaded", function () {
    const resendBtn = document.getElementById("resendBtn");
    const disableTime = 30; // seconds
    const storageKey = "otpResendTimestamp";
    function updateButtonState() {
        const lastClick = localStorage.getItem(storageKey);
        if (lastClick) {
            const elapsed = (Date.now() - Number(lastClick)) / 1000;
            if (elapsed < disableTime) {
                const remaining = Math.ceil(disableTime - elapsed);
                resendBtn.disabled = true;
                resendBtn.textContent = `Resend OTP (${remaining}s)`;
            } else {
                resendBtn.disabled = false;
                resendBtn.textContent = "Resend OTP";
                localStorage.removeItem(storageKey);
            }
        }
    }

    updateButtonState();
    setInterval(updateButtonState, 1000);

    resendBtn.addEventListener("click", function () {
        localStorage.setItem(storageKey, Date.now());
    });
});