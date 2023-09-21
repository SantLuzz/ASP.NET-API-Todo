using Todo.Data;

var builder = WebApplication.CreateBuilder(args);

//configurando os meus controllers
builder.Services.AddControllers();
//configurando o meu dbContext como serviço
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

//pegando os mapas dos controllers
app.MapControllers();

app.Run();

