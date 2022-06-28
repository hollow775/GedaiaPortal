using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GedaiaPortal.Models;

public class AutomationRequest
{
    public long Id { get; set; }
    [DisplayName("Nome")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Name { get; set; }
    
    [DisplayName("Descrição da automação")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Description { get; set; }
    
    [DisplayName("Criado em")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [DisplayName("Última atualização")]
    public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    
    [DisplayName("Usuário")]
    public string User { get; set; }
}
//dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
//dotnet-aspnet-codegenerator controller -name AutomationRequestController -dc ApplicationDbContext -m AutomationRequest --useDefaultLayout --useSqlite --referenceScriptLibraries
//dotnet ef migrations add CreateAutomationRequestTable
//dotnet ef database update