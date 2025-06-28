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

### 2. .env

```bash
GOOGLE_CLIENT_ID=your-client-id
GOOGLE_CLIENT_SECRET=your-client-secret
GOOGLE_CALLBACK_URL=/auth/google/callback
GOOGLE_AUDIENCE=your-client-id
MYSQL_CONNECTION_STRING=server=your-server;user=your-username;password=your-password;database=your_db
ADMIN_WHITELIST=admin1@example.com,admin2@example.com
```

### 3. Install & Build 
```bash
dotnet restore
dotnet build
```

### 4. Run 
```bash
dotnet run
```



