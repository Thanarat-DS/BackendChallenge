# BackendChallenge

## How to Compile and Start the Application  
Ensure you have the following installed:  
- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with `.NET 8.0` workload  

**Clone the Repository**

1. Open Visual Studio

2. Git --> Clone Repository
   
<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/howto1.png"></img>

3. Enter Repository URLโดยใช้ URL นี้:

```sh
https://github.com/Thanarat-DS/BackendChallenge.git
```
   
<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/howto2.png"></img>


## Database
เลือกใช้ <b>SQLite</b> เพราะไม่ต้องเชื่อม Server

ผู้ที่เข้ามาดู Repository นี้ สามารถลองโหลดและ Run โปรเจกต์นี้ได้ทันที

## Model
<b>User</b> Model (Extend from <a href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser?view=aspnetcore-1.1">IdentityUser</a>)
~~~ C#
public class User : IdentityUser
{
    public string Fullname { get; set; } = string.Empty;
}
~~~

<b>Book</b> Model
~~~ C#
public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookId { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("subtitle")]
    public string Subtitle { get; set; } = string.Empty;

    [JsonProperty("isbn13")]
    public string Isbn13 { get; set; } = string.Empty;

    [JsonProperty("price")]
    public string Price { get; set; } = string.Empty;

    [JsonProperty("image")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
}
~~~

<b>UserLike</b> Model
~~~ C#
public class UserLike
{
    public string UserId { get; set; }
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}
~~~

<b>OnModelCreating</b>
~~~ C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
   base.OnModelCreating(modelBuilder);

   modelBuilder.Entity<UserLike>().HasKey(ul => new { ul.UserId, ul.BookId });

   // relationship between UserLike and User
   modelBuilder.Entity<UserLike>()
       .HasOne(ul => ul.User)
       .WithMany()
       .HasForeignKey(ul => ul.UserId);

   // relationship between UserLike and Book
   modelBuilder.Entity<UserLike>()
       .HasOne(ul => ul.Book)
       .WithMany()
       .HasForeignKey(ul => ul.BookId);

}
~~~

## 1. POST /login
This is the user authentication API
Request: {username: xxx , password: xxxx}

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/request2.png"></img>

result:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/result2.png"></img>

## 2. POST /register
Create a user account and store user information into database
 Request: {username:xxxx, password: xxxx, fullname:xxxx}

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/request1.png"></img>

result:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/result1.png"></img>

this will save to <b>AspNetUsers</b> Table:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/IdentityUsertable.png"></img>
 
## 3. GET /books
Get the list of books from https://api.itbook.store/1.0/search/mysql and returns the list sorted
to alphabet (a-z) by book title

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/request3.png"></img>

result:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/result3_1.png"></img>

this will save to <b>Book</b> Table:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/booktable.png"></img>

## 4. POST: /user/like
Like book and store the book that the user like in the database
Request: { user_id: xxx , book_id: 1} 

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/request4.png"></img>

result:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/result4.png"></img>

this will save to <b>UserLike</b> Table:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/userliketable.png"></img>

## Extra
โปรเจคนี้ได้ Implement การสร้าง JWT (JSON Web Tokens) ในการเก็บข้อมูล Login ของผู้ใช้
~~~ C#
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    var user = await _userManager.FindByNameAsync(request.Username);
    if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
    {
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            SecurityAlgorithms.HmacSha256
            )
        );

        return Ok(new { message = "Login successful", Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    return Unauthorized(new { message = "Invalid username or password" });
~~~

result:

<img src="https://raw.githubusercontent.com/Thanarat-DS/BackendChallenge/refs/heads/master/Asset/result2.png"></img>
