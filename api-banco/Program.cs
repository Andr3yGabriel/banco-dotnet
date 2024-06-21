using api_banco.Infraestructure;
using api_banco.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do serviço de controladores para lidar com solicitações HTTP

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Adição de um serviço transient de InterfaceClienteRepository com a implementação ClienteRepository
builder.Services.AddTransient<InterfaceClienteRepository, ClienteRepository>();

// Adição de um serviço transient de InterfaceMovimentacaoRepository com a implementação MovimentacaoRepository
builder.Services.AddTransient<InterfaceMovimentacaoRepository, MovimentacaoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração do CORS para permitir solicitações de qualquer origem, método e cabeçalho
app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Adiciona o middleware de autorização à pipeline de execução da aplicação
app.UseAuthorization();

// Mapeia os controladores para as rotas da API
app.MapControllers();

// Inicia a execução da aplicação
app.Run();