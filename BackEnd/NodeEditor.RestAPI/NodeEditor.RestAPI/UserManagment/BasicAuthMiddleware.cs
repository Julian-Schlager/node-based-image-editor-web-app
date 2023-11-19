namespace NodeEditor.RestAPI.UserManagment;
// https://jasonwatmore.com/post/2021/12/20/net-6-basic-authentication-tutorial-with-example-api

using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using NodeEditor.BuisnessLogic.Interfaces;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        //skip authorization if action is decorated with [AllowAnonymous] attribute
        bool allowAnonymous = context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() is Object;
        if (allowAnonymous)
            await _next(context);
        else
        {
            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                string username = credentials[0];
                string password = credentials[1];

                // authenticate credentials with user service and attach user to http context
                context.Items["User"] = await userService.LogIn(username, password);
            }
            catch
            {
                // do nothing if invalid auth header
                // user is not attached to context so request won't have access to secure routes
            }

            await _next(context);
        }
    }
}
