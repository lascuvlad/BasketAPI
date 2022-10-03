using AutoMapper;
using BasketAPI.Models;
using BasketAPI.Models.Details;
using BasketAPI.Models.Requests;

namespace BasketAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBasketRequest, Basket>();
            CreateMap<AddBasketArticleRequest, Article>();
            CreateMap<UpdateBasketStatusRequest, Basket>();
            CreateMap<Basket, BasketDetails>()
            .ForMember(s => s.Articles, opt => opt.MapFrom(d => d.Article));
            CreateMap<BasketDetails, Basket>()
            .ForMember(s => s.Article, opt => opt.MapFrom(d => d.Articles));
            CreateMap<Article, ArticleDetails>();
            CreateMap<ArticleDetails, Article>();
        }
    }
}
