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
    /// SyncModelSyncedItemProgress
    /// </summary>
    [DataContract]
        public partial class SyncModelSyncedItemProgress :  IEquatable<SyncModelSyncedItemProgress>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncModelSyncedItemProgress" /> class.
        /// </summary>
        /// <param name="progress">progress.</param>
        /// <param name="status">status.</param>
        public SyncModelSyncedItemProgress(double? progress = default(double?), SyncModelSyncJobItemStatus status = default(SyncModelSyncJobItemStatus))
        {
            this.Progress = progress;
            this.Status = status;
        }
        
        /// <summary>
        /// Gets or Sets Progress
        /// </summary>
        [DataMember(Name="Progress", EmitDefaultValue=false)]
        public double? Progress { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="Status", EmitDefaultValue=false)]
        public SyncModelSyncJobItemStatus Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SyncModelSyncedItemProgress {\n");
            sb.Append("  Progress: ").Append(Progress).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as SyncModelSyncedItemProgress);
        }

        /// <summary>
        /// Returns true if SyncModelSyncedItemProgress instances are equal
        /// </summary>
        /// <param name="input">Instance of SyncModelSyncedItemProgress to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SyncModelSyncedItemProgress input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Progress == input.Progress ||
                    (this.Progress != null &&
                    this.Progress.Equals(input.Progress))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Progress != null)
                    hashCode = hashCode * 59 + this.Progress.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                return hashCode;
            }
        }

    }
}
