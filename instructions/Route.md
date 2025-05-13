# MVC的路由程序

如项目结构所示，你可以看到Models，Views和Controllers三个文件夹。

1. 当用户向你的MVC应用程序发起一个请求时，由路由程序进行参数匹配，首先匹配Controller
2. 如果匹配成功，路由程序会将请求转发到对应的Controller
	（如本项目的`PartyController`和`UserController`）
3. Controller通常为一个class，它会进一步匹配class中定义的方法
4. 如果匹配成功，Controller会调用对应的方法，并进行参数绑定

举个例子：

``` csharp
app.MapControllerRoute(name: "default", pattern: "{controller=Party}/{action=Index}/{id?}");
```

如上所示，

- 当用户请求 `/` （也就是什么路径都没有）时  
    路由程序会将请求转发到`PartyController`的`Index`方法  
	因为`controller`具有默认值`Party`， 而`action`具有默认值`Index`
- 当用户请求 `/User` 时  
	路由程序会将请求转发到`UserController`的`Index`方法  
- 当用户请求 `/User/Details/233` 时  
	路由程序会将请求转发到`UserController`的`Details`方法  
	同时，将`233`值尝试作为`id`参数传递给`Details`方法
- 当用户请求 `/Party/Register` 时  
	路由程序会将请求转发到`PartyController`的`Register`方法  
	但是，`Register`方法要求参数`id`，而此处并没有提供  
	绑定失败，未找到合适的路由，返回404 NOT FOUND

## 默认路由

``` csharp
app.MapDefaultControllerRoute();
```

上述方法等价于

``` csharp
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
```