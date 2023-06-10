using Kyoto.Domain.TemplateMessage;
using Microsoft.EntityFrameworkCore;
using TemplateMessageDal = Kyoto.Database.CommonModels.TemplateMessage;

namespace Kyoto.Database.Repositories.TemplateMessage;

public class TemplateMessageRepository : ITemplateMessageRepository
{
    private readonly IDatabaseContext _databaseContext;

    public TemplateMessageRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Domain.TemplateMessage.TemplateMessage> GetAsync(
        TemplateMessageTypeValue templateMessageTypeValue)
    {
        var templateMessageTypeDal = await _databaseContext
            .Set<TemplateMessageDal>()
            .Include(x=>x.TemplateMessageType)
            .FirstAsync(x => x.TemplateMessageType.Code == (int)templateMessageTypeValue);
        
        return templateMessageTypeDal.ToDomain();
    }
    
    public async Task UpdateAsync(
        TemplateMessageTypeValue templateMessageTypeValue,
        string newText)
    {
        var templateMessageTypeDal = await _databaseContext
            .Set<TemplateMessageDal>()
            .Include(x=>x.TemplateMessageType)
            .FirstAsync(x => x.TemplateMessageType.Code == (int)templateMessageTypeValue);

        templateMessageTypeDal.Text = newText;
        _databaseContext.Update(templateMessageTypeDal);
        await _databaseContext.SaveChangesAsync();
    }
}