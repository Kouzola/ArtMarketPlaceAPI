using ArtMarketPlaceAPI.Dto.Request.Validators;
using ArtMarketPlaceAPI.ExceptionHandlers;
using Business_Layer.Services;
using Business_Layer.Validators;
using Data_Access_Layer.AppDbContext;
using Data_Access_Layer.Repositories;
using Domain_Layer.Interfaces.Category;
using Domain_Layer.Interfaces.Customization;
using Domain_Layer.Interfaces.Inquiry;
using Domain_Layer.Interfaces.Order;
using Domain_Layer.Interfaces.PaymentDetails;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.Shipment;
using Domain_Layer.Interfaces.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddDbContext<ArtMarketPlaceDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("ArtMarketPlaceDb"));
    });

//Repositories
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IInquiryRepository,InquiryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomizationRepository,CustomizationRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IPaymentDetailsRepository, PaymentDetailsRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICustomizationService, CustomizationService>();
builder.Services.AddScoped<IShipmentService,ShipmentService>();
builder.Services.AddScoped<IPaymentDetailsService,PaymentDetailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
//ExceptionHandler
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<BusinessExceptionHandler>();
builder.Services.AddExceptionHandler<AlreadyExistsExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//Validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserRequestForAdminValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<InquiryRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserSelfUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomizationRequestValidtor>();

//Authentification
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                    ValidAudience = builder.Configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!))
                };
            });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type=ReferenceType.SecurityScheme,
                                        Id="Bearer"
                                    }
                                },
                                new string[]{}
                            }
                        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Contents"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
