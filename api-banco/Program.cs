using api_banco.Infraestructure;
using api_banco.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do servi�o de controladores para lidar com solicita��es HTTP

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Adi��o de um servi�o transient de InterfaceClienteRepository com a implementa��o ClienteRepository
builder.Services.AddTransient<InterfaceClienteRepository, ClienteRepository>();

// Adi��o de um servi�o transient de InterfaceMovimentacaoRepository com a implementa��o MovimentacaoRepository
builder.Services.AddTransient<InterfaceMovimentacaoRepository, MovimentacaoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura��o do CORS para permitir solicita��es de qualquer origem, m�todo e cabe�alho
app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Adiciona o middleware de autoriza��o � pipeline de execu��o da aplica��o
app.UseAuthorization();

// Mapeia os controladores para as rotas da API
app.MapControllers();

// Inicia a execu��o da aplica��o
app.Run();