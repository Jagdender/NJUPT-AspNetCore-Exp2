# Entity Framework Core

因为期末考试没有填空与选择，此处不介绍EFCore是什么，直接上代码。

## 咋配置啊

如`Program.cs`中所示
``` csharp
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});
```
其中，
- `DatabaseContext`是用户自定义的数据库上下文类
- `UseSqlite`是用户所选择的数据库提供程序所定义的扩展方法，此处为`SQLite`
- `GetConnectionString`是从配置文件中获取连接字符串的方法  
    此时你的`appsettings.json`或`appsettings.Development.json`应该存在类似下列所示的内容
    ``` json
    {
      "ConnectionStrings": {
        "Database": "Data Source=database.db;"
      }
    }
    ```
    `Database`是连接字符串的名称，不同数据库具有不同的ConnectionString格式，请注意。

## 配置好了，咋使啊

我们回到最上面提到的`DatabaseContext`类说起。

这个类是你自定义的一个类，它大抵长成这个样子：
``` csharp
public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    // ...
}
```
你不需要管`DbContextOptions`和`DbContext`是什么，应付期末考试照着这么抄就行了。

然后在这个class内，按照如下格式定义你在数据库中需要的表
``` csharp
public DbSet<Party> Parties { get; set; } = null!;
public DbSet<User> Users { get; set; } = null!;
```
(null!可加可不加，为的是避免编译器warning)

其中，`Party`和`User`是你自定义的类，用于表示数据库中一个关系的结构数据  

例如，在数据库中存在关系`parties`  
其具有`id` `name` `location` `date`四个属性  

那么相应地，你可以在C#中以同样的形式定义一个类`Party`，形同如下
``` csharp
public class Party
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateOnly Date { get; set; }
}
```

`User`同理，在此不再赘述。

## 关系

Entity Framework Core具有非常灵活且便捷的关系配置功能  
在默认情况下就足以应付期末考试的内容

### 一对一关系

例如车票与乘车人，商店与店主的关系等

你可以仿照如下格式定义
``` csharp
public sealed class Ticket
{
    public Passenger Passenger { get; set; } = null!;
}

public sealed class Passenger
{
    public Ticket Ticket { get; set; } = null!;
}
```

更多复杂的关系配置考试大抵不会考到就不再赘述。

### 一对多关系

比如家庭成员的关系，一个家庭可以有多个孩子，但一个孩子只能对应一个家庭

``` csharp
public sealed class Family
{
    public List<Child> Children { get; set; } =[];
}

public sealed class Child
{
    // 此处写不写其实无所谓
    public Family Family { get; set; } = null!;
}
```

更多复杂的关系配置考试大抵不会考到就不再赘述。

### 多对多关系

比如本项目中`User`和`Party`的关系，一个用户可以参加多个聚会，一个聚会也可以有多个用户参加
``` csharp
public sealed class User
{
    public List<Party> Parties { get; set; } = [];
}

public sealed class Party
{
    public List<User> Users { get; set; } = [];
}
```

更多复杂的关系配置考试大抵不会考到就不再赘述。


## 应用

在你需要的地方，使用依赖注入，即构造函数注入上面自定义的`DatabaseContext`类

``` csharp
public class PartyController : Controller
{
    private readonly DatabaseContext context;
    public PartyController(DatabaseContext context)
    {
        this.context = context;
    }
    // ...
}
```
或者
``` csharp
public class PartyController(DatabaseContext context) : Controller
{
    // ...
}
```

你可以使用不限于以下形式的方法来操作数据库  
具体有关LINQ的用法请参考[微软官方文档](https://learn.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/)

- 列出所有
    ``` csharp
    public async Task<IActionResult> Index()
    {
        var parties = await context.Parties.ToListAsync();
        return View(parties);
    }
    ```
- 查找
    ``` csharp
    public async Task<IActionResult> Details(int id)
    {
        var party = await context.Parties.FindAsync(id);

        //或者

        var party_ = await context.Parties
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (party is null)
        {
            return NotFound();
        }
        return View(party);
    }
    ```
- 修改数据库  
    **请注意，任何对数据库的修改都需要调用`SaveChangesAsync`方法**  
    例如：
    ``` csharp
    public async Task<IActionResult> Create(Party party)
    {
        await context.Parties.AddAsync(party);
        await context.SaveChangesAsync(); // 不要遗漏
        return RedirectToAction(nameof(Index));
    }
    ```