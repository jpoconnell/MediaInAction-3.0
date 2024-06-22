/*
 * EmbyClient.Dotnet
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using EmbyClient.Dotnet.Client;
using EmbyClient.Dotnet.Model;

namespace EmbyClient.Dotnet.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
        public interface IVideoHlsServiceApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// No authentication required
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        void GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer (string segmentContainer, string segmentId, string id, string playlistId);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// No authentication required
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerWithHttpInfo (string segmentContainer, string segmentId, string id, string playlistId);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Requires authentication as user
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns></returns>
        void GetVideosByIdLiveM3u8 (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Requires authentication as user
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> GetVideosByIdLiveM3u8WithHttpInfo (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// No authentication required
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerAsync (string segmentContainer, string segmentId, string id, string playlistId);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// No authentication required
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerAsyncWithHttpInfo (string segmentContainer, string segmentId, string id, string playlistId);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Requires authentication as user
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task GetVideosByIdLiveM3u8Async (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Requires authentication as user
        /// </remarks>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> GetVideosByIdLiveM3u8AsyncWithHttpInfo (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
        public partial class VideoHlsServiceApi : IVideoHlsServiceApi
    {
        private EmbyClient.Dotnet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoHlsServiceApi"/> class.
        /// </summary>
        /// <returns></returns>
        public VideoHlsServiceApi(String basePath)
        {
            this.Configuration = new EmbyClient.Dotnet.Client.Configuration { BasePath = basePath };

            ExceptionFactory = EmbyClient.Dotnet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoHlsServiceApi"/> class
        /// </summary>
        /// <returns></returns>
        public VideoHlsServiceApi()
        {
            this.Configuration = EmbyClient.Dotnet.Client.Configuration.Default;

            ExceptionFactory = EmbyClient.Dotnet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoHlsServiceApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public VideoHlsServiceApi(EmbyClient.Dotnet.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = EmbyClient.Dotnet.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = EmbyClient.Dotnet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public EmbyClient.Dotnet.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public EmbyClient.Dotnet.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public IDictionary<String, String> DefaultHeader()
        {
            return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        ///  No authentication required
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public void GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer (string segmentContainer, string segmentId, string id, string playlistId)
        {
             GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerWithHttpInfo(segmentContainer, segmentId, id, playlistId);
        }

        /// <summary>
        ///  No authentication required
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerWithHttpInfo (string segmentContainer, string segmentId, string id, string playlistId)
        {
            // verify the required parameter 'segmentContainer' is set
            if (segmentContainer == null)
                throw new ApiException(400, "Missing required parameter 'segmentContainer' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");
            // verify the required parameter 'segmentId' is set
            if (segmentId == null)
                throw new ApiException(400, "Missing required parameter 'segmentId' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");
            // verify the required parameter 'playlistId' is set
            if (playlistId == null)
                throw new ApiException(400, "Missing required parameter 'playlistId' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");

            var localVarPath = "/Videos/{Id}/hls/{PlaylistId}/{SegmentId}.{SegmentContainer}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (segmentContainer != null) localVarPathParams.Add("SegmentContainer", this.Configuration.ApiClient.ParameterToString(segmentContainer)); // path parameter
            if (segmentId != null) localVarPathParams.Add("SegmentId", this.Configuration.ApiClient.ParameterToString(segmentId)); // path parameter
            if (id != null) localVarPathParams.Add("Id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (playlistId != null) localVarPathParams.Add("PlaylistId", this.Configuration.ApiClient.ParameterToString(playlistId)); // path parameter

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                null);
        }

        /// <summary>
        ///  No authentication required
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerAsync (string segmentContainer, string segmentId, string id, string playlistId)
        {
             await GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerAsyncWithHttpInfo(segmentContainer, segmentId, id, playlistId);

        }

        /// <summary>
        ///  No authentication required
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="segmentContainer"></param>
        /// <param name="segmentId"></param>
        /// <param name="id"></param>
        /// <param name="playlistId"></param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainerAsyncWithHttpInfo (string segmentContainer, string segmentId, string id, string playlistId)
        {
            // verify the required parameter 'segmentContainer' is set
            if (segmentContainer == null)
                throw new ApiException(400, "Missing required parameter 'segmentContainer' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");
            // verify the required parameter 'segmentId' is set
            if (segmentId == null)
                throw new ApiException(400, "Missing required parameter 'segmentId' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");
            // verify the required parameter 'playlistId' is set
            if (playlistId == null)
                throw new ApiException(400, "Missing required parameter 'playlistId' when calling VideoHlsServiceApi->GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer");

            var localVarPath = "/Videos/{Id}/hls/{PlaylistId}/{SegmentId}.{SegmentContainer}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (segmentContainer != null) localVarPathParams.Add("SegmentContainer", this.Configuration.ApiClient.ParameterToString(segmentContainer)); // path parameter
            if (segmentId != null) localVarPathParams.Add("SegmentId", this.Configuration.ApiClient.ParameterToString(segmentId)); // path parameter
            if (id != null) localVarPathParams.Add("Id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (playlistId != null) localVarPathParams.Add("PlaylistId", this.Configuration.ApiClient.ParameterToString(playlistId)); // path parameter

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetVideosByIdHlsByPlaylistidBySegmentidBySegmentcontainer", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                null);
        }

        /// <summary>
        ///  Requires authentication as user
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns></returns>
        public void GetVideosByIdLiveM3u8 (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex)
        {
             GetVideosByIdLiveM3u8WithHttpInfo(id, container, deviceProfileId, deviceId, audioCodec, enableAutoStreamCopy, audioSampleRate, audioBitRate, audioChannels, maxAudioChannels, _static, profile, level, framerate, maxFramerate, copyTimestamps, startTimeTicks, width, height, maxWidth, maxHeight, videoBitRate, subtitleStreamIndex, subtitleMethod, maxRefFrames, maxVideoBitDepth, videoCodec, audioStreamIndex, videoStreamIndex);
        }

        /// <summary>
        ///  Requires authentication as user
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> GetVideosByIdLiveM3u8WithHttpInfo (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling VideoHlsServiceApi->GetVideosByIdLiveM3u8");
            // verify the required parameter 'container' is set
            if (container == null)
                throw new ApiException(400, "Missing required parameter 'container' when calling VideoHlsServiceApi->GetVideosByIdLiveM3u8");

            var localVarPath = "/Videos/{Id}/live.m3u8";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (id != null) localVarPathParams.Add("Id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (deviceProfileId != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "DeviceProfileId", deviceProfileId)); // query parameter
            if (deviceId != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "DeviceId", deviceId)); // query parameter
            if (container != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Container", container)); // query parameter
            if (audioCodec != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioCodec", audioCodec)); // query parameter
            if (enableAutoStreamCopy != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "EnableAutoStreamCopy", enableAutoStreamCopy)); // query parameter
            if (audioSampleRate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioSampleRate", audioSampleRate)); // query parameter
            if (audioBitRate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioBitRate", audioBitRate)); // query parameter
            if (audioChannels != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioChannels", audioChannels)); // query parameter
            if (maxAudioChannels != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxAudioChannels", maxAudioChannels)); // query parameter
            if (_static != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Static", _static)); // query parameter
            if (profile != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Profile", profile)); // query parameter
            if (level != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Level", level)); // query parameter
            if (framerate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Framerate", framerate)); // query parameter
            if (maxFramerate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxFramerate", maxFramerate)); // query parameter
            if (copyTimestamps != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "CopyTimestamps", copyTimestamps)); // query parameter
            if (startTimeTicks != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "StartTimeTicks", startTimeTicks)); // query parameter
            if (width != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Width", width)); // query parameter
            if (height != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Height", height)); // query parameter
            if (maxWidth != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxWidth", maxWidth)); // query parameter
            if (maxHeight != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxHeight", maxHeight)); // query parameter
            if (videoBitRate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "VideoBitRate", videoBitRate)); // query parameter
            if (subtitleStreamIndex != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "SubtitleStreamIndex", subtitleStreamIndex)); // query parameter
            if (subtitleMethod != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "SubtitleMethod", subtitleMethod)); // query parameter
            if (maxRefFrames != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxRefFrames", maxRefFrames)); // query parameter
            if (maxVideoBitDepth != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxVideoBitDepth", maxVideoBitDepth)); // query parameter
            if (videoCodec != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "VideoCodec", videoCodec)); // query parameter
            if (audioStreamIndex != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioStreamIndex", audioStreamIndex)); // query parameter
            if (videoStreamIndex != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "VideoStreamIndex", videoStreamIndex)); // query parameter
            // authentication (apikeyauth) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("api_key")))
            {
                localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "api_key", this.Configuration.GetApiKeyWithPrefix("api_key")));
            }
            // authentication (embyauth) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetVideosByIdLiveM3u8", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                null);
        }

        /// <summary>
        ///  Requires authentication as user
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task GetVideosByIdLiveM3u8Async (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex)
        {
             await GetVideosByIdLiveM3u8AsyncWithHttpInfo(id, container, deviceProfileId, deviceId, audioCodec, enableAutoStreamCopy, audioSampleRate, audioBitRate, audioChannels, maxAudioChannels, _static, profile, level, framerate, maxFramerate, copyTimestamps, startTimeTicks, width, height, maxWidth, maxHeight, videoBitRate, subtitleStreamIndex, subtitleMethod, maxRefFrames, maxVideoBitDepth, videoCodec, audioStreamIndex, videoStreamIndex);

        }

        /// <summary>
        ///  Requires authentication as user
        /// </summary>
        /// <exception cref="EmbyClient.Dotnet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Item Id</param>
        /// <param name="container">Container</param>
        /// <param name="deviceProfileId">Optional. The dlna device profile id to utilize. (optional)</param>
        /// <param name="deviceId">The device id of the client requesting. Used to stop encoding processes when needed. (optional)</param>
        /// <param name="audioCodec">Optional. Specify a audio codec to encode to, e.g. mp3. If omitted the server will auto-select using the url&#x27;s extension. Options: aac, mp3, vorbis, wma. (optional)</param>
        /// <param name="enableAutoStreamCopy">Whether or not to allow automatic stream copy if requested values match the original source. Defaults to true. (optional)</param>
        /// <param name="audioSampleRate">Optional. Specify a specific audio sample rate, e.g. 44100 (optional)</param>
        /// <param name="audioBitRate">Optional. Specify an audio bitrate to encode to, e.g. 128000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="audioChannels">Optional. Specify a specific number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="maxAudioChannels">Optional. Specify a maximum number of audio channels to encode to, e.g. 2 (optional)</param>
        /// <param name="_static">Optional. If true, the original file will be streamed statically without any encoding. Use either no url extension or the original file extension. true/false (optional)</param>
        /// <param name="profile">Optional. Specify a specific h264 profile, e.g. main, baseline, high. (optional)</param>
        /// <param name="level">Optional. Specify a level for the h264 profile, e.g. 3, 3.1. (optional)</param>
        /// <param name="framerate">Optional. A specific video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="maxFramerate">Optional. A specific maximum video framerate to encode to, e.g. 23.976. Generally this should be omitted unless the device has specific requirements. (optional)</param>
        /// <param name="copyTimestamps">Whether or not to copy timestamps when transcoding with an offset. Defaults to false. (optional)</param>
        /// <param name="startTimeTicks">Optional. Specify a starting offset, in ticks. 1 tick &#x3D; 10000 ms (optional)</param>
        /// <param name="width">Optional. The fixed horizontal resolution of the encoded video. (optional)</param>
        /// <param name="height">Optional. The fixed vertical resolution of the encoded video. (optional)</param>
        /// <param name="maxWidth">Optional. The maximum horizontal resolution of the encoded video. (optional)</param>
        /// <param name="maxHeight">Optional. The maximum vertical resolution of the encoded video. (optional)</param>
        /// <param name="videoBitRate">Optional. Specify a video bitrate to encode to, e.g. 500000. If omitted this will be left to encoder defaults. (optional)</param>
        /// <param name="subtitleStreamIndex">Optional. The index of the subtitle stream to use. If omitted no subtitles will be used. (optional)</param>
        /// <param name="subtitleMethod">Optional. Specify the subtitle delivery method. (optional)</param>
        /// <param name="maxRefFrames">Optional. (optional)</param>
        /// <param name="maxVideoBitDepth">Optional. (optional)</param>
        /// <param name="videoCodec">Optional. Specify a video codec to encode to, e.g. h264. If omitted the server will auto-select using the url&#x27;s extension. Options: h264, mpeg4, theora, vpx, wmv. (optional)</param>
        /// <param name="audioStreamIndex">Optional. The index of the audio stream to use. If omitted the first audio stream will be used. (optional)</param>
        /// <param name="videoStreamIndex">Optional. The index of the video stream to use. If omitted the first video stream will be used. (optional)</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> GetVideosByIdLiveM3u8AsyncWithHttpInfo (string id, string container, string deviceProfileId, string deviceId, string audioCodec, bool? enableAutoStreamCopy, int? audioSampleRate, int? audioBitRate, int? audioChannels, int? maxAudioChannels, bool? _static, string profile, string level, float? framerate, float? maxFramerate, bool? copyTimestamps, long? startTimeTicks, int? width, int? height, int? maxWidth, int? maxHeight, int? videoBitRate, int? subtitleStreamIndex, DlnaSubtitleDeliveryMethod subtitleMethod, int? maxRefFrames, int? maxVideoBitDepth, string videoCodec, int? audioStreamIndex, int? videoStreamIndex)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling VideoHlsServiceApi->GetVideosByIdLiveM3u8");
            // verify the required parameter 'container' is set
            if (container == null)
                throw new ApiException(400, "Missing required parameter 'container' when calling VideoHlsServiceApi->GetVideosByIdLiveM3u8");

            var localVarPath = "/Videos/{Id}/live.m3u8";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (id != null) localVarPathParams.Add("Id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (deviceProfileId != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "DeviceProfileId", deviceProfileId)); // query parameter
            if (deviceId != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "DeviceId", deviceId)); // query parameter
            if (container != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Container", container)); // query parameter
            if (audioCodec != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioCodec", audioCodec)); // query parameter
            if (enableAutoStreamCopy != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "EnableAutoStreamCopy", enableAutoStreamCopy)); // query parameter
            if (audioSampleRate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioSampleRate", audioSampleRate)); // query parameter
            if (audioBitRate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioBitRate", audioBitRate)); // query parameter
            if (audioChannels != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioChannels", audioChannels)); // query parameter
            if (maxAudioChannels != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxAudioChannels", maxAudioChannels)); // query parameter
            if (_static != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Static", _static)); // query parameter
            if (profile != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Profile", profile)); // query parameter
            if (level != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Level", level)); // query parameter
            if (framerate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Framerate", framerate)); // query parameter
            if (maxFramerate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxFramerate", maxFramerate)); // query parameter
            if (copyTimestamps != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "CopyTimestamps", copyTimestamps)); // query parameter
            if (startTimeTicks != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "StartTimeTicks", startTimeTicks)); // query parameter
            if (width != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Width", width)); // query parameter
            if (height != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "Height", height)); // query parameter
            if (maxWidth != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxWidth", maxWidth)); // query parameter
            if (maxHeight != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxHeight", maxHeight)); // query parameter
            if (videoBitRate != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "VideoBitRate", videoBitRate)); // query parameter
            if (subtitleStreamIndex != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "SubtitleStreamIndex", subtitleStreamIndex)); // query parameter
            if (subtitleMethod != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "SubtitleMethod", subtitleMethod)); // query parameter
            if (maxRefFrames != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxRefFrames", maxRefFrames)); // query parameter
            if (maxVideoBitDepth != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "MaxVideoBitDepth", maxVideoBitDepth)); // query parameter
            if (videoCodec != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "VideoCodec", videoCodec)); // query parameter
            if (audioStreamIndex != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "AudioStreamIndex", audioStreamIndex)); // query parameter
            if (videoStreamIndex != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "VideoStreamIndex", videoStreamIndex)); // query parameter
            // authentication (apikeyauth) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("api_key")))
            {
                localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "api_key", this.Configuration.GetApiKeyWithPrefix("api_key")));
            }
            // authentication (embyauth) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetVideosByIdLiveM3u8", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                null);
        }

    }
}
