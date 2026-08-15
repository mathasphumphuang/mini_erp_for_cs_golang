using CoreErpService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. เพิ่มการรองรับ Controllers (สำคัญมากสำหรับสถาปัตยกรรมแบบองค์กร)
builder.Services.AddControllers();

// 2. ตั้งค่า Swagger / OpenAPI (สำหรับใช้ทดสอบ API ผ่านหน้าเว็บ)
// .NET 10 ใช้ AddOpenApi() เป็นค่าเริ่มต้น
builder.Services.AddOpenApi();
// แต่ถ้าอยากได้หน้า UI สวยๆ แบบเดิม แนะนำให้ใช้ Swagger ด้วยคำสั่งนี้แทน:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. เสียบสายเชื่อมต่อ Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    // เปิดใช้งาน Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // ของ .NET 10 ถ้าอยากลองใช้ตัวใหม่
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 5. ชี้เป้าให้ระบบรู้จักว่าเราจะใช้ Controller
app.MapControllers();

app.Run();