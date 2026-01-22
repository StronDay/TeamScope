using Microsoft.EntityFrameworkCore;

namespace api.Models;

/// <summary>
/// Контекст базы данных приложения API.
/// Наследуется от DbContext Entity Framework Core для работы с базой данных.
/// </summary>
/// <remarks>
/// Этот класс представляет сессию с базой данных и позволяет:
/// - Определять модели сущностей через DbSet свойства
/// - Настраивать отношения между сущностями в OnModelCreating
/// - Отслеживать изменения объектов
/// - Сохранять и извлекать данные из базы
/// </remarks>
public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    /// <summary>
    /// Коллекция сотрудников в базе данных.
    /// Каждый элемент DbSet соответствует таблице "Employee" в базе данных.
    /// </summary>
    /// <value>Доступ к таблице сотрудников для CRUD операций.</value>
    public DbSet<Employee> Employee { get; set; } = null!;

    /// <summary>
    /// Настраивает модель данных при создании контекста.
    /// Вызывается автоматически EF Core при инициализации контекста.
    /// </summary>
    /// <param name="modelBuilder">Строитель модели для конфигурации сущностей.</param>
    /// <remarks>
    /// В этом методе определяются:
    /// - Первичные ключи (HasKey)
    /// - Ограничения полей (HasMaxLength)
    /// - Связи между сущностями
    /// - Индексы
    /// - Значения по умолчанию
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasKey(x => x.Id); // Id - первичный ключ
        
        modelBuilder.Entity<Employee>().Property(x => x.FirstName).HasMaxLength(20);  // Имя
        modelBuilder.Entity<Employee>().Property(x => x.LastName).HasMaxLength(20);   // Фамилия
        modelBuilder.Entity<Employee>().Property(x => x.Patronymic).HasMaxLength(20); // Отчество
        modelBuilder.Entity<Employee>().Property(x => x.Email).HasMaxLength(20);      // Email

        base.OnModelCreating(modelBuilder); // Вызов базовой реализации
    }
}