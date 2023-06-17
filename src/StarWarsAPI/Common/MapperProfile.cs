using AutoMapper;
using StarWarsAPI.DTOs;
using StarWarsAPI.Entities;

namespace StarWarsAPI.Common;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<CharacterDto, Character>()
            .ReverseMap();

        CreateMap<CharacterCardDto, Character>()
            .ReverseMap();

        CreateMap<MovieDto, Movie>()
            .ReverseMap();

        CreateMap<UpdateCharacterDto, Character>()
            .ReverseMap();

        CreateMap<CreateCharacterDto, Character>()
            .ReverseMap();
    }
}