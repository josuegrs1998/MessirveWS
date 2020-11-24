using MessirveWS.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace MessirveWS.Controllers
{
    public class TokenController : ApiController
    {
        [System.Web.Http.HttpPost]

        public IHttpActionResult Authenticate(Usuario usuario)
        {
            if (usuario == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            /*Buscar usuario con clave en base de datos, si encuentra isCredentialValid a true*/

            string baseURL = "https://localhost:44331/";
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            HttpResponseMessage response = httpClient.GetAsync($"api/Login?Username={usuario.Username}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            bool isCredentialValid = false;
            if (data != "[]")
            {
                List<User> user = JsonConvert.DeserializeObject<List<User>>(data);
                string result = string.Empty;
                byte[] decryted = Convert.FromBase64String(user[0].Password);
            
                result = System.Text.Encoding.Unicode.GetString(decryted);
                user[0].Password = result;
                if(user[0].Password == usuario.Password)
                {
                    isCredentialValid = true;
                }
            }
            
            /**/
         
            if (isCredentialValid)
            {
                var token = GenerateTokenJWT();
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }

        }

        private Token GenerateTokenJWT()
        {
            var now = DateTime.UtcNow;

            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                notBefore: now,
                expires: now.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials
                );

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            Token token = new Token();
            token.AccessToken = jwtTokenString;
            token.ExpiresIn = Convert.ToInt32(expireTime) * 60;

            return token;
        }
    }
}