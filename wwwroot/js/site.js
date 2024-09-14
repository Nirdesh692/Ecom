document.getElementById('imgInput').addEventListener('change', function (event) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('imgPreview');
        output.src = reader.result;
        output.style.display = 'block'; 
    };
    if (event.target.files[0]) {
        reader.readAsDataURL(event.target.files[0]); 
    } else {
        document.getElementById('imgPreview').style.display = 'none'; 
    }
});
