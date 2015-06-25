using System;
using System.Collections.Generic;
using OpenMetaverse;

namespace PlexView
{
	public class NearbyAvatarTracker
	{
		private User user;
		private Dictionary<uint, Avatar> nearbyAvatars = new Dictionary<uint, Avatar>();

		public NearbyAvatarTracker(User user)
		{
			this.user = user;
			this.user.Objects.AvatarUpdate += new EventHandler<AvatarUpdateEventArgs>(Objects_AvatarUpdate);
			this.user.Objects.KillObject += new EventHandler<KillObjectEventArgs>(Objects_KillObject);
			this.user.Self.TeleportProgress += new EventHandler<TeleportEventArgs>(Self_TeleportProgress);
			this.user.Objects.ObjectUpdate += new EventHandler<PrimEventArgs>(Objects_ObjectUpdate);
		}

		public Dictionary<uint, Avatar> GetNearbyAvatars()
		{
			return nearbyAvatars;
		}

		#region eventHandlers

		void Self_TeleportProgress(object sender, TeleportEventArgs e)
		{
			if (e.Status == TeleportStatus.Finished)
			{
				this.nearbyAvatars.Clear();
			}
		}

		void Objects_AvatarUpdate(object sender, AvatarUpdateEventArgs e)
		{
			// Check if we already know about this avatar. If not, add it and announce the callback.
			// Otherwise just update the cache with the new information (e.g. changed position)
			if (!this.nearbyAvatars.ContainsKey(e.Avatar.LocalID))
			{
				lock(this.nearbyAvatars) this.nearbyAvatars.Add(e.Avatar.LocalID, e.Avatar);
				//@todo trigger avatar added event
			}
			else
			{
				lock(this.nearbyAvatars) this.nearbyAvatars[e.Avatar.LocalID] = e.Avatar;
			}
		}

		void Objects_ObjectUpdate(object sender, PrimEventArgs e)
		{
			// If we know of this avatar, update its position and rotation, and send an AvatarUpdated callback.
			if (this.nearbyAvatars.ContainsKey(e.Prim.LocalID))
			{
				Avatar avatar;
				lock (this.nearbyAvatars) avatar = this.nearbyAvatars[e.Prim.LocalID];
				avatar.Position = e.Prim.Position;
				avatar.Rotation = e.Prim.Rotation;
			}
		}
		
		void Objects_KillObject(object sender, KillObjectEventArgs e)
		{
			// If we know of this avatar, remove it and announce its loss.
			lock (this.nearbyAvatars)
			{
				if (nearbyAvatars.ContainsKey(e.ObjectLocalID))
				{
					Avatar avatar = this.nearbyAvatars[e.ObjectLocalID];
					this.nearbyAvatars.Remove(e.ObjectLocalID);
					//@todo trigger avatar removed event
				}
			}
		}

		#endregion
	}
}

