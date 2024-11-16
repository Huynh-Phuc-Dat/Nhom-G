const MESSAGE_STATUS = {
    success: "Success",
    error: "Error",
    warning: "Warning"
}

const isNullOrEmpty = (str) => str === null || str === "" || str === undefined;
const convertFiletoBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = reject;
});

const formatDate = (date) => {
    if (isNullOrEmpty(date)) {
        return null;
    }
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}/${month}/${day}`;
}

const formatMoney = (number) => {
    if (isNullOrEmpty(number)) {
        return '';
    }
    return number.toLocaleString('vi-VN');
}

const swalCommon = async (type, text, isShowCancelButton = true) => {
    const { value } = await Swal.fire({
        text: text,
        icon: type === "success" ? "success" : "warning",
        showCancelButton: isShowCancelButton,
        buttonsStyling: false,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Không",
        customClass: {
            confirmButton: "btn fw-bold btn-primary",
            cancelButton: "btn fw-bold btn-active-light-primary"
        }
    })

    return value;

}
