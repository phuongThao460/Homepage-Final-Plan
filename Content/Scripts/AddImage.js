function ChangeImage(uploadImage, previewImg) {
    if (uploadImage.files && uploadImage.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImg).attr('src', e.target.result);
        }
        reader.readAsDataURL(uploadImage.files[0]);
    }
}