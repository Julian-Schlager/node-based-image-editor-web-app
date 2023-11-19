namespace NodeEditor.RestAPI.UserManagment;
// https://jasonwatmore.com/post/2021/12/20/net-6-basic-authentication-tutorial-with-example-api

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{ }
