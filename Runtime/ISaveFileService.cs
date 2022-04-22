// <copyright file="ISaveFileService.cs" company="Luke Parker">
// Copyright (c) Luke Parker. All rights reserved.
// </copyright>

namespace LPSoft.SaveFileService
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the Save File Service.
    /// </summary>
    public interface ISaveFileService
    {
        /// <summary>
        /// Sets a specified slot with the new save data, overwriting what was in the internal state.
        /// </summary>
        /// <param name="index">Index of the data to update.</param>
        /// <param name="newData">The new save data.</param>
        /// <returns>A task representing the async method.</returns>
        Task Set(int index, string newData);

        /// <summary>
        /// Gets a slot of save data from the internal state.
        /// </summary>
        /// <param name="index">The index of the slot.</param>
        /// <returns>The save data in the slot of the provided index.</returns>
        Task<string> Get(int index);

        /// <summary>
        /// Saves all the current slots to file.
        /// </summary>
        /// <param name="path">The file path to save to.</param>
        /// <returns>A task representing the async method.</returns>
        Task Save(string path);

        /// <summary>
        /// Loads file slots into the internal state.
        /// </summary>
        /// <param name="path">The file path to load from.</param>
        /// <returns>A task representing the async method.</returns>
        Task Load(string path);
    }
}
