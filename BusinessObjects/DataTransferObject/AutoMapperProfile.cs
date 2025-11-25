using AutoMapper;
using BusinessObjects.Entity;

namespace BusinessObjects.DataTransferObject
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Artist, ArtistResponse>();
            CreateMap<ArtistResponse, Artist>();

            CreateMap<Artist, ArtistSummary>();
            CreateMap<ArtistSummary, Artist>();

            CreateMap<Artist, ArtistRequest>();
            CreateMap<ArtistRequest, Artist>();


            CreateMap<Track, TrackResponse>();
            CreateMap<TrackResponse, Track>();

            CreateMap<Track, TrackSummary>();
            CreateMap<TrackSummary, Track>();

            CreateMap<Track, TrackRequest>();
            CreateMap<TrackRequest, Track>();


            CreateMap<Record, RecordResponse>();
            CreateMap<RecordResponse, Record>();

            CreateMap<Record, RecordSummary>();
            CreateMap<RecordSummary, Record>();

            CreateMap<Record, RecordRequest>();
            CreateMap<RecordRequest, Record>();
        }
    }
}