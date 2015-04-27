How to impersonate user (for testing purpose) on ASP.NET application with Windows authentication
================================================================================================

The goal of this application is to show you how to impersonate the connected Windows user on an ASP.NET application which is using Windows authentication (usually an Intranet application).

The Windows authentication (NTLM) is enabled in the Web.config:

```
  <system.web>
    <authentication mode="Windows" />
  </system.web>
```

**How to impersonate another user**:

- Open the ASP.NET application project [DemoImpersonateWindowsUserAspnet.sln](https://github.com/ubikuity/impersonate-windows-user-aspnet/DemoImpersonateWindowsUserAspnet/DemoImpersonateWindowsUserAspnet.sln)
- Run the ASP.NET application (Ctrl+F5)
- To impersonate a user, add in the URL **/LogAs?user=anotherUser** (where "anotherUser" is the Windows username of the person you want to impersonate in the application)  
- You can act within the application as the impersonated user
- To log out, type in the URL **/LogAs?reset**

**Notes**:

- **For security/audit reasons, you should not give the ability to impersonate a user on Production.**
- The username of the user to impersonate should be typed without the Windows domain name: /LogAs?user=johnDoe (and not /LogAs?user=MYDOMAIN\johnDoe)

**Technical information:**

- The trick is to create a cookie containing the username we want to impersonate, then after normal Windows autentication, we redefine on the fly HttpContext.Current.User for each new request (see Global.asax.cs).

**References:**

- Inspired by [http://www.hanselman.com/blog/SystemThreadingThreadCurrentPrincipalVsSystemWebHttpContextCurrentUserOrWhyFormsAuthenticationCanBeSubtle.aspx](http://www.hanselman.com/blog/SystemThreadingThreadCurrentPrincipalVsSystemWebHttpContextCurrentUserOrWhyFormsAuthenticationCanBeSubtle.aspx)
