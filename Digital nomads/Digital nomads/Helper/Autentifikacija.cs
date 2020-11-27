using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Http;
namespace Digital_nomads.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";
        public static void SetLogiraniKorisnik(this HttpContext context,LogiraniKorisnikSesija korisnik,bool snimiUCookie=false)
        {
            context.Session.SetObjectAsJson(LogiraniKorisnik, korisnik);
            if (snimiUCookie)
                context.Response.SetCookieJson(LogiraniKorisnik, korisnik);
            else context.Response.SetCookieJson(LogiraniKorisnik, null);
        }
        public static LogiraniKorisnikSesija GetLogiraniKorisnik(this HttpContext context)
        {
            LogiraniKorisnikSesija korisnik = context.Session.GetObjectFromJson<LogiraniKorisnikSesija>(LogiraniKorisnik);
            if (korisnik==null)
            {
                korisnik = context.Request.GetCookieJson<LogiraniKorisnikSesija>(LogiraniKorisnik);
                context.Session.SetObjectAsJson(LogiraniKorisnik, korisnik);
            }
            return korisnik;
        }
    }
}
