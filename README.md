# 📰 Akhbarna API — أخبارنا

> **REST API** لتجميع وإدارة الأخبار العربية من أكبر المصادر الإخبارية العالمية والعربية

🌐 **Live Demo:** [http://akhbarna1.runasp.net/swagger/index.html](http://akhbarna1.runasp.net/swagger/index.html)

---

## 💡 فكرة المشروع

**أخبارنا** هو Backend API مبني بـ ASP.NET Core يقوم بـ:

- جمع الأخبار **تلقائياً** من مصادر إخبارية عربية وعالمية عبر **RSS Feeds**
- تصنيف الأخبار حسب **الفئة** (سياسة، رياضة، اقتصاد، تكنولوجيا، صحة، فن، دولي)
- توفير نظام **مستخدمين** مع تسجيل دخول وصلاحيات
- إمكانية **حفظ المقالات** والاشتراك في مصادر معينة
- إرسال **إشعارات** للمستخدمين
- لوحة تحكم لـ **Admin** لإدارة المحتوى

---

## 🛠️ التقنيات المستخدمة

| التقنية | الاستخدام |
|--------|-----------|
| **ASP.NET Core 8** | إطار العمل الرئيسي |
| **Entity Framework Core** | ORM للتعامل مع قاعدة البيانات |
| **SQL Server** | قاعدة البيانات |
| **JWT Authentication** | نظام تسجيل الدخول والصلاحيات |
| **RSS Feed Parsing** | جلب الأخبار تلقائياً |
| **Swagger / OpenAPI** | توثيق الـ API |
| **IIS / runasp.net** | استضافة المشروع |

---

## 📡 مصادر الأخبار

| المصدر | الفئات |
|--------|--------|
| 🔴 **CNN عربي** | سياسة، رياضة، اقتصاد، تكنولوجيا، صحة، فن، دولي |
| 🔵 **BBC عربي** | سياسة، اقتصاد، رياضة، فن، تكنولوجيا، دولي |
| 🟡 **سكاي نيوز عربية** | سياسة، رياضة، اقتصاد، صحة، تكنولوجيا، فن |
| 🟢 **DW عربي** | سياسة، اقتصاد، تكنولوجيا، دولي |
| 🔵 **فرانس 24** | سياسة، اقتصاد، رياضة، دولي |
| 📰 **المصري اليوم** | سياسة، دولي |

---

## 📂 هيكل المشروع

```
AkhabarnaAPI/
├── Controllers/        # API Endpoints
│   ├── ArticleController.cs
│   ├── AuthController.cs
│   ├── CategoriesController.cs
│   ├── NotificationController.cs
│   └── SavedArticlesController.cs
├── Models/             # Database Models
├── DTOs/               # Data Transfer Objects
├── Services/           # Business Logic
├── Repositories/       # Data Access Layer
├── Helper/             # JWT & Utilities
└── Migrations/         # EF Core Migrations
```

---

## 🔗 أهم Endpoints

### 📰 الأخبار (Articles)
| Method | Endpoint | الوصف |
|--------|----------|-------|
| GET | `/api/article/static` | كل الأخبار |
| GET | `/api/article/static?category=رياضة` | أخبار حسب الفئة |
| GET | `/api/article/static/breaking` | الأخبار العاجلة |
| GET | `/api/article/static/most-read` | الأكثر قراءة |
| GET | `/api/article/static/latest` | آخر الأخبار |
| GET | `/api/article/static/by-category` | الأخبار مقسمة بالفئات |
| GET | `/api/article/static/sections` | الأخبار في أقسام |

### 👤 المستخدمين (Auth)
| Method | Endpoint | الوصف |
|--------|----------|-------|
| POST | `/api/auth/register` | تسجيل مستخدم جديد |
| POST | `/api/auth/login` | تسجيل الدخول |
| POST | `/api/auth/forgot-password` | نسيت كلمة المرور |

---

## 🚀 تشغيل المشروع محلياً

```bash
# 1. Clone the repo
git clone https://github.com/Mahmoud3178/AkhabarnaAPI.git

# 2. تعديل connection string في appsettings.json
# "DefaultConnection": "Server=...;Database=AkhabarnaDB;..."

# 3. تطبيق الـ Migrations
dotnet ef database update

# 4. تشغيل المشروع
dotnet run
```

---

## 👨‍💻 المطور

**Mahmoud** — Full Stack Developer

---

## 📄 License
This project is open source and available under the [MIT License](LICENSE).
