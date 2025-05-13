<h1 align="center"> 
  <br>
  南京邮电大学 ASP.NET应用开发 实验二
  <br>
</h1>

<h3 align="center">MVC 应用开发</h3>

## 实验要求(2025)

要求开发一个MVC应用，具体要求如下：

- 要求使用数据库管理所有聚会信息和登记信息。 
- 用户打开主页，看到聚会信息和登记按钮。 
- 点击某个聚会旁边的登记按钮，打开登记页面。提交登记表单后显示确认页面。注意，确认页面会显示之前填写的用户名，并包含一个超链接指向显示该聚会所有登记信息的页面。 
- 点击聚会信息旁边的“Show Attenders”按钮，显示该聚会的所有登记信息。 
- 要求登记信息中的邮箱作为用户唯一标识。点击页面中的邮箱，显示该用户登记了哪些聚会。

## Getting Start

1. clone该仓库
    ```bash
	git clone https://github.com/Jagdender/NJUPT-AspNetCore-Exp2.git
	```
2. 确保本地有`Entity Framework Core` CLI 工具
	```bash
	dotnet tool install --global dotnet-ef
	```
3. 建立数据库迁移
	```bash
	dotnet ef migrations add InitialCreate
	```
4. 执行迁移
	```bash
	dotnet ef database update
	```

按上述顺序执行命令，完成数据库的初始化和更新。

## 关于复习(2025)

有关该项目的结构解析与相关考点已经整理至 [此处](instructions/main.md) 。

请配合本项目源码使用。