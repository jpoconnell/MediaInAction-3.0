/*
 * EmbyClient.Dotnet
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwaggerDateConverter = EmbyClient.Dotnet.Client.SwaggerDateConverter;

namespace EmbyClient.Dotnet.Model
{
    /// <summary>
    /// Defines Emby.Media.Model.Enums.VideoMediaTypes
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
        public enum EmbyMediaModelEnumsVideoMediaTypes
    {
        /// <summary>
        /// Enum Unknown for value: Unknown
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown = 1,
        /// <summary>
        /// Enum Copy for value: copy
        /// </summary>
        [EnumMember(Value = "copy")]
        Copy = 2,
        /// <summary>
        /// Enum Flv1 for value: flv1
        /// </summary>
        [EnumMember(Value = "flv1")]
        Flv1 = 3,
        /// <summary>
        /// Enum H263 for value: h263
        /// </summary>
        [EnumMember(Value = "h263")]
        H263 = 4,
        /// <summary>
        /// Enum H263p for value: h263p
        /// </summary>
        [EnumMember(Value = "h263p")]
        H263p = 5,
        /// <summary>
        /// Enum H264 for value: h264
        /// </summary>
        [EnumMember(Value = "h264")]
        H264 = 6,
        /// <summary>
        /// Enum Hevc for value: hevc
        /// </summary>
        [EnumMember(Value = "hevc")]
        Hevc = 7,
        /// <summary>
        /// Enum Mjpeg for value: mjpeg
        /// </summary>
        [EnumMember(Value = "mjpeg")]
        Mjpeg = 8,
        /// <summary>
        /// Enum Mpeg1video for value: mpeg1video
        /// </summary>
        [EnumMember(Value = "mpeg1video")]
        Mpeg1video = 9,
        /// <summary>
        /// Enum Mpeg2video for value: mpeg2video
        /// </summary>
        [EnumMember(Value = "mpeg2video")]
        Mpeg2video = 10,
        /// <summary>
        /// Enum Mpeg4 for value: mpeg4
        /// </summary>
        [EnumMember(Value = "mpeg4")]
        Mpeg4 = 11,
        /// <summary>
        /// Enum Msvideo1 for value: msvideo1
        /// </summary>
        [EnumMember(Value = "msvideo1")]
        Msvideo1 = 12,
        /// <summary>
        /// Enum Theora for value: theora
        /// </summary>
        [EnumMember(Value = "theora")]
        Theora = 13,
        /// <summary>
        /// Enum Vc1image for value: vc1image
        /// </summary>
        [EnumMember(Value = "vc1image")]
        Vc1image = 14,
        /// <summary>
        /// Enum Vc1 for value: vc1
        /// </summary>
        [EnumMember(Value = "vc1")]
        Vc1 = 15,
        /// <summary>
        /// Enum Vp8 for value: vp8
        /// </summary>
        [EnumMember(Value = "vp8")]
        Vp8 = 16,
        /// <summary>
        /// Enum Vp9 for value: vp9
        /// </summary>
        [EnumMember(Value = "vp9")]
        Vp9 = 17,
        /// <summary>
        /// Enum Wmv1 for value: wmv1
        /// </summary>
        [EnumMember(Value = "wmv1")]
        Wmv1 = 18,
        /// <summary>
        /// Enum Wmv2 for value: wmv2
        /// </summary>
        [EnumMember(Value = "wmv2")]
        Wmv2 = 19,
        /// <summary>
        /// Enum Wmv3 for value: wmv3
        /// </summary>
        [EnumMember(Value = "wmv3")]
        Wmv3 = 20,
        /// <summary>
        /// Enum _012v for value: _012v
        /// </summary>
        [EnumMember(Value = "_012v")]
        _012v = 21,
        /// <summary>
        /// Enum _4xm for value: _4xm
        /// </summary>
        [EnumMember(Value = "_4xm")]
        _4xm = 22,
        /// <summary>
        /// Enum _8bps for value: _8bps
        /// </summary>
        [EnumMember(Value = "_8bps")]
        _8bps = 23,
        /// <summary>
        /// Enum A64multi for value: a64_multi
        /// </summary>
        [EnumMember(Value = "a64_multi")]
        A64multi = 24,
        /// <summary>
        /// Enum A64multi5 for value: a64_multi5
        /// </summary>
        [EnumMember(Value = "a64_multi5")]
        A64multi5 = 25,
        /// <summary>
        /// Enum Aasc for value: aasc
        /// </summary>
        [EnumMember(Value = "aasc")]
        Aasc = 26,
        /// <summary>
        /// Enum Aic for value: aic
        /// </summary>
        [EnumMember(Value = "aic")]
        Aic = 27,
        /// <summary>
        /// Enum Aliaspix for value: alias_pix
        /// </summary>
        [EnumMember(Value = "alias_pix")]
        Aliaspix = 28,
        /// <summary>
        /// Enum Amv for value: amv
        /// </summary>
        [EnumMember(Value = "amv")]
        Amv = 29,
        /// <summary>
        /// Enum Anm for value: anm
        /// </summary>
        [EnumMember(Value = "anm")]
        Anm = 30,
        /// <summary>
        /// Enum Ansi for value: ansi
        /// </summary>
        [EnumMember(Value = "ansi")]
        Ansi = 31,
        /// <summary>
        /// Enum Apng for value: apng
        /// </summary>
        [EnumMember(Value = "apng")]
        Apng = 32,
        /// <summary>
        /// Enum Asv1 for value: asv1
        /// </summary>
        [EnumMember(Value = "asv1")]
        Asv1 = 33,
        /// <summary>
        /// Enum Asv2 for value: asv2
        /// </summary>
        [EnumMember(Value = "asv2")]
        Asv2 = 34,
        /// <summary>
        /// Enum Aura for value: aura
        /// </summary>
        [EnumMember(Value = "aura")]
        Aura = 35,
        /// <summary>
        /// Enum Aura2 for value: aura2
        /// </summary>
        [EnumMember(Value = "aura2")]
        Aura2 = 36,
        /// <summary>
        /// Enum Av1 for value: av1
        /// </summary>
        [EnumMember(Value = "av1")]
        Av1 = 37,
        /// <summary>
        /// Enum Avrn for value: avrn
        /// </summary>
        [EnumMember(Value = "avrn")]
        Avrn = 38,
        /// <summary>
        /// Enum Avrp for value: avrp
        /// </summary>
        [EnumMember(Value = "avrp")]
        Avrp = 39,
        /// <summary>
        /// Enum Avs for value: avs
        /// </summary>
        [EnumMember(Value = "avs")]
        Avs = 40,
        /// <summary>
        /// Enum Avui for value: avui
        /// </summary>
        [EnumMember(Value = "avui")]
        Avui = 41,
        /// <summary>
        /// Enum Ayuv for value: ayuv
        /// </summary>
        [EnumMember(Value = "ayuv")]
        Ayuv = 42,
        /// <summary>
        /// Enum Bethsoftvid for value: bethsoftvid
        /// </summary>
        [EnumMember(Value = "bethsoftvid")]
        Bethsoftvid = 43,
        /// <summary>
        /// Enum Bfi for value: bfi
        /// </summary>
        [EnumMember(Value = "bfi")]
        Bfi = 44,
        /// <summary>
        /// Enum Binkvideo for value: binkvideo
        /// </summary>
        [EnumMember(Value = "binkvideo")]
        Binkvideo = 45,
        /// <summary>
        /// Enum Bintext for value: bintext
        /// </summary>
        [EnumMember(Value = "bintext")]
        Bintext = 46,
        /// <summary>
        /// Enum Bitpacked for value: bitpacked
        /// </summary>
        [EnumMember(Value = "bitpacked")]
        Bitpacked = 47,
        /// <summary>
        /// Enum Bmp for value: bmp
        /// </summary>
        [EnumMember(Value = "bmp")]
        Bmp = 48,
        /// <summary>
        /// Enum Bmvvideo for value: bmv_video
        /// </summary>
        [EnumMember(Value = "bmv_video")]
        Bmvvideo = 49,
        /// <summary>
        /// Enum Brenderpix for value: brender_pix
        /// </summary>
        [EnumMember(Value = "brender_pix")]
        Brenderpix = 50,
        /// <summary>
        /// Enum C93 for value: c93
        /// </summary>
        [EnumMember(Value = "c93")]
        C93 = 51,
        /// <summary>
        /// Enum Cavs for value: cavs
        /// </summary>
        [EnumMember(Value = "cavs")]
        Cavs = 52,
        /// <summary>
        /// Enum Cdgraphics for value: cdgraphics
        /// </summary>
        [EnumMember(Value = "cdgraphics")]
        Cdgraphics = 53,
        /// <summary>
        /// Enum Cdxl for value: cdxl
        /// </summary>
        [EnumMember(Value = "cdxl")]
        Cdxl = 54,
        /// <summary>
        /// Enum Cfhd for value: cfhd
        /// </summary>
        [EnumMember(Value = "cfhd")]
        Cfhd = 55,
        /// <summary>
        /// Enum Cinepak for value: cinepak
        /// </summary>
        [EnumMember(Value = "cinepak")]
        Cinepak = 56,
        /// <summary>
        /// Enum Clearvideo for value: clearvideo
        /// </summary>
        [EnumMember(Value = "clearvideo")]
        Clearvideo = 57,
        /// <summary>
        /// Enum Cljr for value: cljr
        /// </summary>
        [EnumMember(Value = "cljr")]
        Cljr = 58,
        /// <summary>
        /// Enum Cllc for value: cllc
        /// </summary>
        [EnumMember(Value = "cllc")]
        Cllc = 59,
        /// <summary>
        /// Enum Cmv for value: cmv
        /// </summary>
        [EnumMember(Value = "cmv")]
        Cmv = 60,
        /// <summary>
        /// Enum Cpia for value: cpia
        /// </summary>
        [EnumMember(Value = "cpia")]
        Cpia = 61,
        /// <summary>
        /// Enum Cscd for value: cscd
        /// </summary>
        [EnumMember(Value = "cscd")]
        Cscd = 62,
        /// <summary>
        /// Enum Cyuv for value: cyuv
        /// </summary>
        [EnumMember(Value = "cyuv")]
        Cyuv = 63,
        /// <summary>
        /// Enum Daala for value: daala
        /// </summary>
        [EnumMember(Value = "daala")]
        Daala = 64,
        /// <summary>
        /// Enum Dds for value: dds
        /// </summary>
        [EnumMember(Value = "dds")]
        Dds = 65,
        /// <summary>
        /// Enum Dfa for value: dfa
        /// </summary>
        [EnumMember(Value = "dfa")]
        Dfa = 66,
        /// <summary>
        /// Enum Dirac for value: dirac
        /// </summary>
        [EnumMember(Value = "dirac")]
        Dirac = 67,
        /// <summary>
        /// Enum Dnxhd for value: dnxhd
        /// </summary>
        [EnumMember(Value = "dnxhd")]
        Dnxhd = 68,
        /// <summary>
        /// Enum Dpx for value: dpx
        /// </summary>
        [EnumMember(Value = "dpx")]
        Dpx = 69,
        /// <summary>
        /// Enum Dsicinvideo for value: dsicinvideo
        /// </summary>
        [EnumMember(Value = "dsicinvideo")]
        Dsicinvideo = 70,
        /// <summary>
        /// Enum Dvvideo for value: dvvideo
        /// </summary>
        [EnumMember(Value = "dvvideo")]
        Dvvideo = 71,
        /// <summary>
        /// Enum Dxa for value: dxa
        /// </summary>
        [EnumMember(Value = "dxa")]
        Dxa = 72,
        /// <summary>
        /// Enum Dxtory for value: dxtory
        /// </summary>
        [EnumMember(Value = "dxtory")]
        Dxtory = 73,
        /// <summary>
        /// Enum Dxv for value: dxv
        /// </summary>
        [EnumMember(Value = "dxv")]
        Dxv = 74,
        /// <summary>
        /// Enum Escape124 for value: escape124
        /// </summary>
        [EnumMember(Value = "escape124")]
        Escape124 = 75,
        /// <summary>
        /// Enum Escape130 for value: escape130
        /// </summary>
        [EnumMember(Value = "escape130")]
        Escape130 = 76,
        /// <summary>
        /// Enum Exr for value: exr
        /// </summary>
        [EnumMember(Value = "exr")]
        Exr = 77,
        /// <summary>
        /// Enum Ffv1 for value: ffv1
        /// </summary>
        [EnumMember(Value = "ffv1")]
        Ffv1 = 78,
        /// <summary>
        /// Enum Ffvhuff for value: ffvhuff
        /// </summary>
        [EnumMember(Value = "ffvhuff")]
        Ffvhuff = 79,
        /// <summary>
        /// Enum Fic for value: fic
        /// </summary>
        [EnumMember(Value = "fic")]
        Fic = 80,
        /// <summary>
        /// Enum Fits for value: fits
        /// </summary>
        [EnumMember(Value = "fits")]
        Fits = 81,
        /// <summary>
        /// Enum Flashsv for value: flashsv
        /// </summary>
        [EnumMember(Value = "flashsv")]
        Flashsv = 82,
        /// <summary>
        /// Enum Flashsv2 for value: flashsv2
        /// </summary>
        [EnumMember(Value = "flashsv2")]
        Flashsv2 = 83,
        /// <summary>
        /// Enum Flic for value: flic
        /// </summary>
        [EnumMember(Value = "flic")]
        Flic = 84,
        /// <summary>
        /// Enum Fmvc for value: fmvc
        /// </summary>
        [EnumMember(Value = "fmvc")]
        Fmvc = 85,
        /// <summary>
        /// Enum Fraps for value: fraps
        /// </summary>
        [EnumMember(Value = "fraps")]
        Fraps = 86,
        /// <summary>
        /// Enum Frwu for value: frwu
        /// </summary>
        [EnumMember(Value = "frwu")]
        Frwu = 87,
        /// <summary>
        /// Enum G2m for value: g2m
        /// </summary>
        [EnumMember(Value = "g2m")]
        G2m = 88,
        /// <summary>
        /// Enum Gdv for value: gdv
        /// </summary>
        [EnumMember(Value = "gdv")]
        Gdv = 89,
        /// <summary>
        /// Enum Gif for value: gif
        /// </summary>
        [EnumMember(Value = "gif")]
        Gif = 90,
        /// <summary>
        /// Enum H261 for value: h261
        /// </summary>
        [EnumMember(Value = "h261")]
        H261 = 91,
        /// <summary>
        /// Enum H263i for value: h263i
        /// </summary>
        [EnumMember(Value = "h263i")]
        H263i = 92,
        /// <summary>
        /// Enum Hap for value: hap
        /// </summary>
        [EnumMember(Value = "hap")]
        Hap = 93,
        /// <summary>
        /// Enum Hnm4video for value: hnm4video
        /// </summary>
        [EnumMember(Value = "hnm4video")]
        Hnm4video = 94,
        /// <summary>
        /// Enum Hqhqa for value: hq_hqa
        /// </summary>
        [EnumMember(Value = "hq_hqa")]
        Hqhqa = 95,
        /// <summary>
        /// Enum Hqx for value: hqx
        /// </summary>
        [EnumMember(Value = "hqx")]
        Hqx = 96,
        /// <summary>
        /// Enum Huffyuv for value: huffyuv
        /// </summary>
        [EnumMember(Value = "huffyuv")]
        Huffyuv = 97,
        /// <summary>
        /// Enum Idcin for value: idcin
        /// </summary>
        [EnumMember(Value = "idcin")]
        Idcin = 98,
        /// <summary>
        /// Enum Idf for value: idf
        /// </summary>
        [EnumMember(Value = "idf")]
        Idf = 99,
        /// <summary>
        /// Enum Iffilbm for value: iff_ilbm
        /// </summary>
        [EnumMember(Value = "iff_ilbm")]
        Iffilbm = 100,
        /// <summary>
        /// Enum Indeo2 for value: indeo2
        /// </summary>
        [EnumMember(Value = "indeo2")]
        Indeo2 = 101,
        /// <summary>
        /// Enum Indeo3 for value: indeo3
        /// </summary>
        [EnumMember(Value = "indeo3")]
        Indeo3 = 102,
        /// <summary>
        /// Enum Indeo4 for value: indeo4
        /// </summary>
        [EnumMember(Value = "indeo4")]
        Indeo4 = 103,
        /// <summary>
        /// Enum Indeo5 for value: indeo5
        /// </summary>
        [EnumMember(Value = "indeo5")]
        Indeo5 = 104,
        /// <summary>
        /// Enum Interplayvideo for value: interplayvideo
        /// </summary>
        [EnumMember(Value = "interplayvideo")]
        Interplayvideo = 105,
        /// <summary>
        /// Enum Jpeg2000 for value: jpeg2000
        /// </summary>
        [EnumMember(Value = "jpeg2000")]
        Jpeg2000 = 106,
        /// <summary>
        /// Enum Jpegls for value: jpegls
        /// </summary>
        [EnumMember(Value = "jpegls")]
        Jpegls = 107,
        /// <summary>
        /// Enum Jv for value: jv
        /// </summary>
        [EnumMember(Value = "jv")]
        Jv = 108,
        /// <summary>
        /// Enum Kgv1 for value: kgv1
        /// </summary>
        [EnumMember(Value = "kgv1")]
        Kgv1 = 109,
        /// <summary>
        /// Enum Kmvc for value: kmvc
        /// </summary>
        [EnumMember(Value = "kmvc")]
        Kmvc = 110,
        /// <summary>
        /// Enum Lagarith for value: lagarith
        /// </summary>
        [EnumMember(Value = "lagarith")]
        Lagarith = 111,
        /// <summary>
        /// Enum Ljpeg for value: ljpeg
        /// </summary>
        [EnumMember(Value = "ljpeg")]
        Ljpeg = 112,
        /// <summary>
        /// Enum Loco for value: loco
        /// </summary>
        [EnumMember(Value = "loco")]
        Loco = 113,
        /// <summary>
        /// Enum M101 for value: m101
        /// </summary>
        [EnumMember(Value = "m101")]
        M101 = 114,
        /// <summary>
        /// Enum Mad for value: mad
        /// </summary>
        [EnumMember(Value = "mad")]
        Mad = 115,
        /// <summary>
        /// Enum Magicyuv for value: magicyuv
        /// </summary>
        [EnumMember(Value = "magicyuv")]
        Magicyuv = 116,
        /// <summary>
        /// Enum Mdec for value: mdec
        /// </summary>
        [EnumMember(Value = "mdec")]
        Mdec = 117,
        /// <summary>
        /// Enum Mimic for value: mimic
        /// </summary>
        [EnumMember(Value = "mimic")]
        Mimic = 118,
        /// <summary>
        /// Enum Mjpegb for value: mjpegb
        /// </summary>
        [EnumMember(Value = "mjpegb")]
        Mjpegb = 119,
        /// <summary>
        /// Enum Mmvideo for value: mmvideo
        /// </summary>
        [EnumMember(Value = "mmvideo")]
        Mmvideo = 120,
        /// <summary>
        /// Enum Motionpixels for value: motionpixels
        /// </summary>
        [EnumMember(Value = "motionpixels")]
        Motionpixels = 121,
        /// <summary>
        /// Enum Msa1 for value: msa1
        /// </summary>
        [EnumMember(Value = "msa1")]
        Msa1 = 122,
        /// <summary>
        /// Enum Mscc for value: mscc
        /// </summary>
        [EnumMember(Value = "mscc")]
        Mscc = 123,
        /// <summary>
        /// Enum Msmpeg4v1 for value: msmpeg4v1
        /// </summary>
        [EnumMember(Value = "msmpeg4v1")]
        Msmpeg4v1 = 124,
        /// <summary>
        /// Enum Msmpeg4v2 for value: msmpeg4v2
        /// </summary>
        [EnumMember(Value = "msmpeg4v2")]
        Msmpeg4v2 = 125,
        /// <summary>
        /// Enum Msmpeg4v3 for value: msmpeg4v3
        /// </summary>
        [EnumMember(Value = "msmpeg4v3")]
        Msmpeg4v3 = 126,
        /// <summary>
        /// Enum Msrle for value: msrle
        /// </summary>
        [EnumMember(Value = "msrle")]
        Msrle = 127,
        /// <summary>
        /// Enum Mss1 for value: mss1
        /// </summary>
        [EnumMember(Value = "mss1")]
        Mss1 = 128,
        /// <summary>
        /// Enum Mss2 for value: mss2
        /// </summary>
        [EnumMember(Value = "mss2")]
        Mss2 = 129,
        /// <summary>
        /// Enum Mszh for value: mszh
        /// </summary>
        [EnumMember(Value = "mszh")]
        Mszh = 130,
        /// <summary>
        /// Enum Mts2 for value: mts2
        /// </summary>
        [EnumMember(Value = "mts2")]
        Mts2 = 131,
        /// <summary>
        /// Enum Mvc1 for value: mvc1
        /// </summary>
        [EnumMember(Value = "mvc1")]
        Mvc1 = 132,
        /// <summary>
        /// Enum Mvc2 for value: mvc2
        /// </summary>
        [EnumMember(Value = "mvc2")]
        Mvc2 = 133,
        /// <summary>
        /// Enum Mxpeg for value: mxpeg
        /// </summary>
        [EnumMember(Value = "mxpeg")]
        Mxpeg = 134,
        /// <summary>
        /// Enum Nuv for value: nuv
        /// </summary>
        [EnumMember(Value = "nuv")]
        Nuv = 135,
        /// <summary>
        /// Enum Pafvideo for value: paf_video
        /// </summary>
        [EnumMember(Value = "paf_video")]
        Pafvideo = 136,
        /// <summary>
        /// Enum Pam for value: pam
        /// </summary>
        [EnumMember(Value = "pam")]
        Pam = 137,
        /// <summary>
        /// Enum Pbm for value: pbm
        /// </summary>
        [EnumMember(Value = "pbm")]
        Pbm = 138,
        /// <summary>
        /// Enum Pcx for value: pcx
        /// </summary>
        [EnumMember(Value = "pcx")]
        Pcx = 139,
        /// <summary>
        /// Enum Pgm for value: pgm
        /// </summary>
        [EnumMember(Value = "pgm")]
        Pgm = 140,
        /// <summary>
        /// Enum Pgmyuv for value: pgmyuv
        /// </summary>
        [EnumMember(Value = "pgmyuv")]
        Pgmyuv = 141,
        /// <summary>
        /// Enum Pictor for value: pictor
        /// </summary>
        [EnumMember(Value = "pictor")]
        Pictor = 142,
        /// <summary>
        /// Enum Pixlet for value: pixlet
        /// </summary>
        [EnumMember(Value = "pixlet")]
        Pixlet = 143,
        /// <summary>
        /// Enum Png for value: png
        /// </summary>
        [EnumMember(Value = "png")]
        Png = 144,
        /// <summary>
        /// Enum Ppm for value: ppm
        /// </summary>
        [EnumMember(Value = "ppm")]
        Ppm = 145,
        /// <summary>
        /// Enum Prores for value: prores
        /// </summary>
        [EnumMember(Value = "prores")]
        Prores = 146,
        /// <summary>
        /// Enum Psd for value: psd
        /// </summary>
        [EnumMember(Value = "psd")]
        Psd = 147,
        /// <summary>
        /// Enum Ptx for value: ptx
        /// </summary>
        [EnumMember(Value = "ptx")]
        Ptx = 148,
        /// <summary>
        /// Enum Qdraw for value: qdraw
        /// </summary>
        [EnumMember(Value = "qdraw")]
        Qdraw = 149,
        /// <summary>
        /// Enum Qpeg for value: qpeg
        /// </summary>
        [EnumMember(Value = "qpeg")]
        Qpeg = 150,
        /// <summary>
        /// Enum Qtrle for value: qtrle
        /// </summary>
        [EnumMember(Value = "qtrle")]
        Qtrle = 151,
        /// <summary>
        /// Enum R10k for value: r10k
        /// </summary>
        [EnumMember(Value = "r10k")]
        R10k = 152,
        /// <summary>
        /// Enum R210 for value: r210
        /// </summary>
        [EnumMember(Value = "r210")]
        R210 = 153,
        /// <summary>
        /// Enum Rawvideo for value: rawvideo
        /// </summary>
        [EnumMember(Value = "rawvideo")]
        Rawvideo = 154,
        /// <summary>
        /// Enum Rl2 for value: rl2
        /// </summary>
        [EnumMember(Value = "rl2")]
        Rl2 = 155,
        /// <summary>
        /// Enum Roq for value: roq
        /// </summary>
        [EnumMember(Value = "roq")]
        Roq = 156,
        /// <summary>
        /// Enum Rpza for value: rpza
        /// </summary>
        [EnumMember(Value = "rpza")]
        Rpza = 157,
        /// <summary>
        /// Enum Rscc for value: rscc
        /// </summary>
        [EnumMember(Value = "rscc")]
        Rscc = 158,
        /// <summary>
        /// Enum Rv10 for value: rv10
        /// </summary>
        [EnumMember(Value = "rv10")]
        Rv10 = 159,
        /// <summary>
        /// Enum Rv20 for value: rv20
        /// </summary>
        [EnumMember(Value = "rv20")]
        Rv20 = 160,
        /// <summary>
        /// Enum Rv30 for value: rv30
        /// </summary>
        [EnumMember(Value = "rv30")]
        Rv30 = 161,
        /// <summary>
        /// Enum Rv40 for value: rv40
        /// </summary>
        [EnumMember(Value = "rv40")]
        Rv40 = 162,
        /// <summary>
        /// Enum Sanm for value: sanm
        /// </summary>
        [EnumMember(Value = "sanm")]
        Sanm = 163,
        /// <summary>
        /// Enum Scpr for value: scpr
        /// </summary>
        [EnumMember(Value = "scpr")]
        Scpr = 164,
        /// <summary>
        /// Enum Screenpresso for value: screenpresso
        /// </summary>
        [EnumMember(Value = "screenpresso")]
        Screenpresso = 165,
        /// <summary>
        /// Enum Sgi for value: sgi
        /// </summary>
        [EnumMember(Value = "sgi")]
        Sgi = 166,
        /// <summary>
        /// Enum Sgirle for value: sgirle
        /// </summary>
        [EnumMember(Value = "sgirle")]
        Sgirle = 167,
        /// <summary>
        /// Enum Sheervideo for value: sheervideo
        /// </summary>
        [EnumMember(Value = "sheervideo")]
        Sheervideo = 168,
        /// <summary>
        /// Enum Smackvideo for value: smackvideo
        /// </summary>
        [EnumMember(Value = "smackvideo")]
        Smackvideo = 169,
        /// <summary>
        /// Enum Smc for value: smc
        /// </summary>
        [EnumMember(Value = "smc")]
        Smc = 170,
        /// <summary>
        /// Enum Smvjpeg for value: smvjpeg
        /// </summary>
        [EnumMember(Value = "smvjpeg")]
        Smvjpeg = 171,
        /// <summary>
        /// Enum Snow for value: snow
        /// </summary>
        [EnumMember(Value = "snow")]
        Snow = 172,
        /// <summary>
        /// Enum Sp5x for value: sp5x
        /// </summary>
        [EnumMember(Value = "sp5x")]
        Sp5x = 173,
        /// <summary>
        /// Enum Speedhq for value: speedhq
        /// </summary>
        [EnumMember(Value = "speedhq")]
        Speedhq = 174,
        /// <summary>
        /// Enum Srgc for value: srgc
        /// </summary>
        [EnumMember(Value = "srgc")]
        Srgc = 175,
        /// <summary>
        /// Enum Sunrast for value: sunrast
        /// </summary>
        [EnumMember(Value = "sunrast")]
        Sunrast = 176,
        /// <summary>
        /// Enum Svg for value: svg
        /// </summary>
        [EnumMember(Value = "svg")]
        Svg = 177,
        /// <summary>
        /// Enum Svq1 for value: svq1
        /// </summary>
        [EnumMember(Value = "svq1")]
        Svq1 = 178,
        /// <summary>
        /// Enum Svq3 for value: svq3
        /// </summary>
        [EnumMember(Value = "svq3")]
        Svq3 = 179,
        /// <summary>
        /// Enum Targa for value: targa
        /// </summary>
        [EnumMember(Value = "targa")]
        Targa = 180,
        /// <summary>
        /// Enum Targay216 for value: targa_y216
        /// </summary>
        [EnumMember(Value = "targa_y216")]
        Targay216 = 181,
        /// <summary>
        /// Enum Tdsc for value: tdsc
        /// </summary>
        [EnumMember(Value = "tdsc")]
        Tdsc = 182,
        /// <summary>
        /// Enum Tgq for value: tgq
        /// </summary>
        [EnumMember(Value = "tgq")]
        Tgq = 183,
        /// <summary>
        /// Enum Tgv for value: tgv
        /// </summary>
        [EnumMember(Value = "tgv")]
        Tgv = 184,
        /// <summary>
        /// Enum Thp for value: thp
        /// </summary>
        [EnumMember(Value = "thp")]
        Thp = 185,
        /// <summary>
        /// Enum Tiertexseqvideo for value: tiertexseqvideo
        /// </summary>
        [EnumMember(Value = "tiertexseqvideo")]
        Tiertexseqvideo = 186,
        /// <summary>
        /// Enum Tiff for value: tiff
        /// </summary>
        [EnumMember(Value = "tiff")]
        Tiff = 187,
        /// <summary>
        /// Enum Tmv for value: tmv
        /// </summary>
        [EnumMember(Value = "tmv")]
        Tmv = 188,
        /// <summary>
        /// Enum Tqi for value: tqi
        /// </summary>
        [EnumMember(Value = "tqi")]
        Tqi = 189,
        /// <summary>
        /// Enum Truemotion1 for value: truemotion1
        /// </summary>
        [EnumMember(Value = "truemotion1")]
        Truemotion1 = 190,
        /// <summary>
        /// Enum Truemotion2 for value: truemotion2
        /// </summary>
        [EnumMember(Value = "truemotion2")]
        Truemotion2 = 191,
        /// <summary>
        /// Enum Truemotion2rt for value: truemotion2rt
        /// </summary>
        [EnumMember(Value = "truemotion2rt")]
        Truemotion2rt = 192,
        /// <summary>
        /// Enum Tscc for value: tscc
        /// </summary>
        [EnumMember(Value = "tscc")]
        Tscc = 193,
        /// <summary>
        /// Enum Tscc2 for value: tscc2
        /// </summary>
        [EnumMember(Value = "tscc2")]
        Tscc2 = 194,
        /// <summary>
        /// Enum Txd for value: txd
        /// </summary>
        [EnumMember(Value = "txd")]
        Txd = 195,
        /// <summary>
        /// Enum Ulti for value: ulti
        /// </summary>
        [EnumMember(Value = "ulti")]
        Ulti = 196,
        /// <summary>
        /// Enum Utvideo for value: utvideo
        /// </summary>
        [EnumMember(Value = "utvideo")]
        Utvideo = 197,
        /// <summary>
        /// Enum V210 for value: v210
        /// </summary>
        [EnumMember(Value = "v210")]
        V210 = 198,
        /// <summary>
        /// Enum V210x for value: v210x
        /// </summary>
        [EnumMember(Value = "v210x")]
        V210x = 199,
        /// <summary>
        /// Enum V308 for value: v308
        /// </summary>
        [EnumMember(Value = "v308")]
        V308 = 200,
        /// <summary>
        /// Enum V408 for value: v408
        /// </summary>
        [EnumMember(Value = "v408")]
        V408 = 201,
        /// <summary>
        /// Enum V410 for value: v410
        /// </summary>
        [EnumMember(Value = "v410")]
        V410 = 202,
        /// <summary>
        /// Enum Vb for value: vb
        /// </summary>
        [EnumMember(Value = "vb")]
        Vb = 203,
        /// <summary>
        /// Enum Vble for value: vble
        /// </summary>
        [EnumMember(Value = "vble")]
        Vble = 204,
        /// <summary>
        /// Enum Vcr1 for value: vcr1
        /// </summary>
        [EnumMember(Value = "vcr1")]
        Vcr1 = 205,
        /// <summary>
        /// Enum Vixl for value: vixl
        /// </summary>
        [EnumMember(Value = "vixl")]
        Vixl = 206,
        /// <summary>
        /// Enum Vmdvideo for value: vmdvideo
        /// </summary>
        [EnumMember(Value = "vmdvideo")]
        Vmdvideo = 207,
        /// <summary>
        /// Enum Vmnc for value: vmnc
        /// </summary>
        [EnumMember(Value = "vmnc")]
        Vmnc = 208,
        /// <summary>
        /// Enum Vp3 for value: vp3
        /// </summary>
        [EnumMember(Value = "vp3")]
        Vp3 = 209,
        /// <summary>
        /// Enum Vp5 for value: vp5
        /// </summary>
        [EnumMember(Value = "vp5")]
        Vp5 = 210,
        /// <summary>
        /// Enum Vp6 for value: vp6
        /// </summary>
        [EnumMember(Value = "vp6")]
        Vp6 = 211,
        /// <summary>
        /// Enum Vp6a for value: vp6a
        /// </summary>
        [EnumMember(Value = "vp6a")]
        Vp6a = 212,
        /// <summary>
        /// Enum Vp6f for value: vp6f
        /// </summary>
        [EnumMember(Value = "vp6f")]
        Vp6f = 213,
        /// <summary>
        /// Enum Vp7 for value: vp7
        /// </summary>
        [EnumMember(Value = "vp7")]
        Vp7 = 214,
        /// <summary>
        /// Enum Webp for value: webp
        /// </summary>
        [EnumMember(Value = "webp")]
        Webp = 215,
        /// <summary>
        /// Enum Wmv3image for value: wmv3image
        /// </summary>
        [EnumMember(Value = "wmv3image")]
        Wmv3image = 216,
        /// <summary>
        /// Enum Wnv1 for value: wnv1
        /// </summary>
        [EnumMember(Value = "wnv1")]
        Wnv1 = 217,
        /// <summary>
        /// Enum Wrappedavframe for value: wrapped_avframe
        /// </summary>
        [EnumMember(Value = "wrapped_avframe")]
        Wrappedavframe = 218,
        /// <summary>
        /// Enum Wsvqa for value: ws_vqa
        /// </summary>
        [EnumMember(Value = "ws_vqa")]
        Wsvqa = 219,
        /// <summary>
        /// Enum Xanwc3 for value: xan_wc3
        /// </summary>
        [EnumMember(Value = "xan_wc3")]
        Xanwc3 = 220,
        /// <summary>
        /// Enum Xanwc4 for value: xan_wc4
        /// </summary>
        [EnumMember(Value = "xan_wc4")]
        Xanwc4 = 221,
        /// <summary>
        /// Enum Xbin for value: xbin
        /// </summary>
        [EnumMember(Value = "xbin")]
        Xbin = 222,
        /// <summary>
        /// Enum Xbm for value: xbm
        /// </summary>
        [EnumMember(Value = "xbm")]
        Xbm = 223,
        /// <summary>
        /// Enum Xface for value: xface
        /// </summary>
        [EnumMember(Value = "xface")]
        Xface = 224,
        /// <summary>
        /// Enum Xpm for value: xpm
        /// </summary>
        [EnumMember(Value = "xpm")]
        Xpm = 225,
        /// <summary>
        /// Enum Xwd for value: xwd
        /// </summary>
        [EnumMember(Value = "xwd")]
        Xwd = 226,
        /// <summary>
        /// Enum Y41p for value: y41p
        /// </summary>
        [EnumMember(Value = "y41p")]
        Y41p = 227,
        /// <summary>
        /// Enum Ylc for value: ylc
        /// </summary>
        [EnumMember(Value = "ylc")]
        Ylc = 228,
        /// <summary>
        /// Enum Yop for value: yop
        /// </summary>
        [EnumMember(Value = "yop")]
        Yop = 229,
        /// <summary>
        /// Enum Yuv4 for value: yuv4
        /// </summary>
        [EnumMember(Value = "yuv4")]
        Yuv4 = 230,
        /// <summary>
        /// Enum Zerocodec for value: zerocodec
        /// </summary>
        [EnumMember(Value = "zerocodec")]
        Zerocodec = 231,
        /// <summary>
        /// Enum Zlib for value: zlib
        /// </summary>
        [EnumMember(Value = "zlib")]
        Zlib = 232,
        /// <summary>
        /// Enum Zmbv for value: zmbv
        /// </summary>
        [EnumMember(Value = "zmbv")]
        Zmbv = 233    }
}
