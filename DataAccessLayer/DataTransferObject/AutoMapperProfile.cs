using AutoMapper;
using BusinessObjects.Entity;

namespace DataAccessLayer.DataTransferObject
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistDto, Artist>();

            CreateMap<Track, TrackDto>();
            CreateMap<TrackDto, Track>();

            CreateMap<Record, RecordDto>();
            CreateMap<RecordDto, Record>();
        }
    }
}