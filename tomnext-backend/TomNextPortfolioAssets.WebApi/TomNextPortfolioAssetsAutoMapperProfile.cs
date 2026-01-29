using AutoMapper;
using System;
using TomNextPortfolioAssets.Domain.Tables;
using TomNextPortfolioAssets.WebApi.Models.ViewModels;

namespace TomNextPortfolioAssets.WebApi
{
    class TomNextPortfolioAssetsAutoMapperProfile : Profile
    {
        public TomNextPortfolioAssetsAutoMapperProfile()
        {
            CreateAssestsMap();
        }

        private void CreateAssestsMap()
        {
            CreateMap<Assets, AssetsViewModel>();
        }
    }
}
