﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homepage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TAIKHOANQUANTRIVIEN
    {
        [Display(Name = "Mã tài khoản")]
        public int ID_QUANTRIVIEN { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string TEN_DANGNHAP { get; set; }

        [Display(Name = "Mật khẩu")]
        public string MATKHAU { get; set; }

        [Display(Name = "Tên")]
        public string TEN_QUANTRIVIEN { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string ANH_DAIDIEN { get; set; }
    }
}
