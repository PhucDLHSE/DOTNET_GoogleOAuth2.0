# DOTNET_GOOGLEOAUTH2.0

✅ Template ASP.NET Core Web API hỗ trợ **Google OAuth 2.0 login** và quản lý người dùng với **Entity Framework Core + MySQL**.  

Dễ dàng tái sử dụng cho các hệ thống có chức năng đăng nhập bằng Google và phân quyền.

---

## 🧩 Tính năng chính

- 🔒 Đăng nhập Google OAuth2.0 (dành cho Mobile App hoặc Web)
- 🧠 Xử lý xác thực bằng `GoogleJsonWebSignature` (Google.Apis.Auth)
- 🗃️ EF Core + MySQL (Pomelo)
- 🧑‍💻 Auto-register người dùng mới từ Google
- 🛂 Hỗ trợ phân quyền động (Admin / Lecturer / Student)
- ⚙️ Cấu hình qua `.env` để dễ nhân bản dự án

---

## 🚀 Bắt đầu sử dụng

### 1. Clone repository
```bash
git clone https://github.com/PHUCDLHSE/DOTNET_GOOGLEOAUTH2.0.git
cd DOTNET_GOOGLEOAUTH2.0
```
### 2. Rename .csproj
```bash
mv DOTNET_GOOGLEOAUTH2.0.csproj your_project_name.csproj
```
### 3. Create .sln and add to .sln
```bash
dotnet new sln -n your_project_name
dotnet sln your_project_name.sln add your_project_name.csproj
```
### 4.  Update RootNamespace in .csproj
```bash
<RootNamespace>your_project_name</RootNamespace>
```
### 5. Find & replace namespace
```bash
DotnetGoogleOAuth2 -> your_project_name
```
### 6. Create .env
```bash
GOOGLE_CLIENT_ID=your-client-id
GOOGLE_CLIENT_SECRET=your-client-secret
GOOGLE_CALLBACK_URL=/auth/google/callback
GOOGLE_AUDIENCE=your-client-id
MYSQL_CONNECTION_STRING=server=your-server;user=your-username;password=your-password;database=your_db
ADMIN_WHITELIST=admin1@example.com,admin2@example.com
```
### 7. Install & Build 
```bash
dotnet restore
dotnet build
```
### 8. Run 
```bash
dotnet run
```



