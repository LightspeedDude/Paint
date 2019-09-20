using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Services
{
    interface IPhotoLibrary
    {
        Task<Stream> PickPhotoAsync();
        Task<bool> SavePhotoAsync(byte[] data, string folder, string filename);


        //Task<bool> SaveBitmap(byte[] bitmapData, string filename);
    }
}
