// <copyright file="SaveService.cs" company="Luke Parker">
// Copyright (c) Luke Parker. All rights reserved.
// </copyright>

namespace LPSoft.SaveFileService
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using UnityEngine;

    /// <inheritdoc />
    public sealed class SaveService<TData> : ISaveFileService<TData>
    {
        private TData[] _maxSlots;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveService{TData}"/> class.
        /// </summary>
        /// <param name="maxSlots">The maximum amount of slots for the service starting from 1.</param>
        public SaveService(int maxSlots)
        {
            if (maxSlots <= 0)
            {
                throw new ArgumentException("Slots cannot be zero or negative.");
            }

            _maxSlots = new TData[maxSlots];
        }

        /// <summary>
        /// Gets the total slots for the save data.
        /// </summary>
        public int TotalSlots => _maxSlots.Length;

        /// <inheritdoc />
        public async Task<TData> Get(int index)
        {
            if (index > _maxSlots.Length)
            {
                throw new ArgumentException("Index provided is greater than maximum provided slots.");
            }

            if (index < 0)
            {
                throw new ArgumentException("Index cannot be negative.");
            }

            return await Task.FromResult(_maxSlots[index]);
        }

        /// <inheritdoc />
        public Task Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path provided is invalid.");
            }

            using (var fs = File.OpenRead(path))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var data = Encoding.UTF8.GetString(reader.ReadBytes((int)fs.Length));
                    var loadedData = JsonUtility.FromJson<DataWrapper<TData>>(data);
                    _maxSlots = loadedData.Data;

                    return Task.CompletedTask;
                }
            }
        }

        /// <inheritdoc />
        public Task Save(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path provided is invalid.");
            }

            using (var fs = File.Create(path))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    var wrapper = new DataWrapper<TData>() {
                        Data = _maxSlots;
                    }
                    var data = JsonUtility.ToJson(wrapper);
                    writer.Write(Encoding.UTF8.GetBytes(data));

                    return Task.CompletedTask;
                }
            }
        }

        /// <inheritdoc />
        public Task Set(int index, TData newData)
        {
            if (index > _maxSlots.Length)
            {
                throw new ArgumentException("Index provided is greater than maximum provided slots.");
            }

            if (index < 0)
            {
                throw new ArgumentException("Index cannot be negative.");
            }

            _maxSlots[index] = newData;
            return Task.CompletedTask;
        }

        private class DataWrapper<T> {
            public T[] Data;
        }
    }
}
