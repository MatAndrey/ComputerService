using ComputerService.Data.Repositories;
using ComputerService.Models;
using ComputerService.ViewModels;

namespace ComputerService.Services
{
    public class NewsService(INewsRepository newsRepository) : INewsService
    {
        public async Task<NewsViewModel> CreateNewsAsync(NewsViewModel model)
        {
            var newsEntity = new News
            {
                Date = DateTime.UtcNow,
                Translations = [
                    new NewsTranslation
                    {
                        LangCode = model.LangCode,
                        Title = model.Title,
                        Content = model.Content
                    }
                ]
            };
            var createdNews = await newsRepository.CreateNewsAsync(newsEntity);
            return new NewsViewModel
            {
                Id = createdNews.Id,
                Date = createdNews.Date,
                LangCode = model.LangCode,
                Title = model.Title,
                Content = model.Content
            };
        }

        public async Task DeleteNewsAsync(int id)
        {
            await newsRepository.DeleteNewsAsync(id);
        }

        public async Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(string langCode)
        {
            return (await newsRepository.GetAllNewsAsync()).Select(n => new
            {
                News = n,
                Translation = n.Translations.FirstOrDefault(t => t.LangCode == langCode) ?? n.Translations.FirstOrDefault()
            }).Select(x => new NewsViewModel
            {
                Id = x.News.Id,
                Date = x.News.Date,
                LangCode = x.Translation?.LangCode ?? "N/A",
                Title = x.Translation?.Title ?? "N/A",
                Content = x.Translation?.Content ?? "N/A"
            });
        }

        public async Task<NewsViewModel> GetNewsByIdAsync(int id, string langCode)
        {
            var news = await newsRepository.GetNewsByIdAsync(id);
            if (news == null) return null;
            var translation = news.Translations.FirstOrDefault(t => t.LangCode == langCode) ?? news.Translations.FirstOrDefault();
            return new NewsViewModel
            {
                Id = news.Id,
                Date = news.Date,
                LangCode = translation?.LangCode ?? "N/A",
                Title = translation?.Title ?? "N/A",
                Content = translation?.Content ?? "N/A"
            };
        }

        public async Task<NewsViewModel> UpdateNewsAsync(NewsViewModel model)
        {
            var dbNews = await newsRepository.GetNewsByIdAsync(model.Id);
            if (dbNews == null) throw new Exception("News not found");
            var translation = dbNews.Translations.FirstOrDefault(t => t.LangCode == model.LangCode);
            if (translation != null)
            {
                translation.Title = model.Title;
                translation.Content = model.Content;
            }
            else
            {
                dbNews.Translations.Add(new NewsTranslation
                {
                    LangCode = model.LangCode,
                    Title = model.Title,
                    Content = model.Content
                });
            }
            await newsRepository.UpdateNewsAsync(dbNews);
            return model;
        }
    }
}
