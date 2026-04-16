using ComputerService.Areas.Admin.ViewModels;
using ComputerService.Data.Repositories;
using ComputerService.Models;

namespace ComputerService.Services
{
    public class PageService(IPageRepository pageRepository) : IPageService
    {
        public async Task<PageViewModel> GetPageAsync(int id, string langCode)
        {
            var page = await pageRepository.GetPageAsync(id);
            if(page == null)
                throw new Exception("Page not found");
            var translation = page.Translations.FirstOrDefault(t => t.LangCode == langCode) ??
                page.Translations.FirstOrDefault();
            return new PageViewModel
            {
                Id = page.Id,
                Content = translation?.Content ?? "N/A",
                LangCode = translation?.LangCode ?? "N/A"
            };
        }

        public async Task<PageViewModel> UpdatePageAsync(PageViewModel model)
        {
            var page = await pageRepository.GetPageAsync(model.Id);
            if (page == null)
                throw new Exception("Page not found");
            var translation = page.Translations.FirstOrDefault(t => t.LangCode == model.LangCode);
            if (translation != null)
            {
                translation.Content = model.Content;
            }
            else
            {
                page.Translations.Add(new PageTranslation
                {
                    LangCode = model.LangCode,
                    Content = model.Content
                });
            }
            await pageRepository.UpdatePageAsync(page);
            return new PageViewModel
            {
                Id = page.Id,
                Content = model.Content,
                LangCode = model.LangCode
            };
        }
    }
}
