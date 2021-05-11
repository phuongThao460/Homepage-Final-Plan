$(document).ready(function(){
    $(".btn_login-in").click(function Validate() {
    if ($("#username").val() == '')
        {
            alert("Tên đăng nhập không được rỗng!");
        }
        else if ($("#password").val() == '')
        {
            alert("Mật khẩu không được rỗng!");
        }
    });
});
function myFunction(){
        $(document).ready(function(){
            $('#search').on('keyup', function(event){
            event.preventDefault();
            var tukhoa = $(this).val().toLowerCase();
            $('#trangchu').filter(function(){
                    $(this).toggle($(this).text.toLowerCase().indexOf(tukhoa) > -1);
            });
            });
        });
        }
/* ------------------------------------ Click on login and Sign Up to  changue and view the effect
---------------------------------------
*/

function cambiar_login() {
document.querySelector('.cont_forms').className = "cont_forms cont_forms_active_login";  
document.querySelector('.cont_form_login').style.display = "block";
document.querySelector('.cont_form_sign_up').style.opacity = "0";               
setTimeout(function(){  document.querySelector('.cont_form_login').style.opacity = "1"; },400);  
setTimeout(function(){    
document.querySelector('.cont_form_sign_up').style.display = "none";
},200);
}

function cambiar_sign_up(at) {
document.querySelector('.cont_forms').className = "cont_forms cont_forms_active_sign_up";
document.querySelector('.cont_form_sign_up').style.display = "block";
document.querySelector('.cont_form_login').style.opacity = "0";
setTimeout(function(){  document.querySelector('.cont_form_sign_up').style.opacity = "1";
},100);  

setTimeout(function(){   document.querySelector('.cont_form_login').style.display = "none";
},400);  


}    
function ocultar_login_sign_up() {

document.querySelector('.cont_forms').className = "cont_forms";  
document.querySelector('.cont_form_sign_up').style.opacity = "0";               
document.querySelector('.cont_form_login').style.opacity = "0"; 

setTimeout(function(){
document.querySelector('.cont_form_sign_up').style.display = "none";
document.querySelector('.cont_form_login').style.display = "none";
},500);  
}
function validationLogInForm()
{
	var user = document.getElementById('username').value;
	var pass = document.getElementById('password').value;
	if(user == '')
	{
		alert("Đại Nhân, ngài quên báo tên rồi kìa!");
	} 
	else if(pass == '')
	{
		alert("Có vẻ như mật khẩu của Ngài không đúng rồi, hãy kiểm tra lại nhé!");
	} 
	else 
	{
		alert("Ngài đã điểm danh thành công, chúc Ngài một ngày tốt lành ^^"); 
		return true;
	}
	return false;
}
function validationSignUpForm()
{
	var username = document.getElementById('userName').value;
	var password = document.getElementById('passWord').value;
	var email = document.getElementById('email').value;
	var confirmPassword = document.getElementById('confirmPassword').value;
	if(email == '')
	{
		alert("Ngài quên báo địa chỉ để gửi thư rồi thưa Đại Nhân");
	}
	else if(username == '')
	{
		alert("Đại Nhân, ngài quên báo tên rồi kìa!");
	} 
	else if(password == '' )
	{
		alert("Có vẻ như mật khẩu của Ngài không đúng rồi, hãy kiểm tra lại nhé!");
	}
	else if(password.length < 8  || password.length > 20)
	{
		alert("about length");
	}
	else if(confirmPassword != password)
	{
		alert("Hừm, có vẻ như mật khẩu xác nhận không đúng rồi, Ngài hãy kiểm tra lại nhé");
	}
	else 
	{
		alert("Ngài đã báo danh thành công, hãy cùng các Âm Dương Sư khác bảo vệ Bình An Kinh này nhé, chúc Ngài một ngày tốt lành ^^"); 
		return true;
	}
	return false;
}