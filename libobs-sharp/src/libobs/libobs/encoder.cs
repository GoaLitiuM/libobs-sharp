/***************************************************************************
	Copyright (C) 2014-2015 by Ari Vuollet <ari.vuollet@kapsi.fi>
	
	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, see <http://www.gnu.org/licenses/>.
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OBS
{
    public static partial class libobs
    {
        /* ------------------------------------------------------------------------- */
        /* Encoders */

        //EXPORT const char *obs_encoder_get_display_name(const char *id);
        //EXPORT obs_encoder_t *obs_video_encoder_create(const char *id, const char *name, obs_data_t *settings);
        //EXPORT obs_encoder_t *obs_audio_encoder_create(const char *id, const char *name, obs_data_t *settings, size_t mixer_idx);
        //EXPORT void obs_encoder_destroy(obs_encoder_t *encoder);
		
		//EXPORT void obs_encoder_set_name(obs_encoder_t *encoder, const char *name);
        //EXPORT const char *obs_encoder_get_name(const obs_encoder_t *encoder);
		//EXPORT const char *obs_get_encoder_codec(const char *id);
		//EXPORT enum obs_encoder_type obs_get_encoder_type(const char *id);
        //EXPORT const char *obs_encoder_get_codec(const obs_encoder_t *encoder);
		//EXPORT enum obs_encoder_type obs_encoder_get_type(const obs_encoder_t *encoder);
        //EXPORT void obs_encoder_set_scaled_size(obs_encoder_t *encoder, uint32_t width, uint32_t height);
        //EXPORT uint32_t obs_encoder_get_width(const obs_encoder_t *encoder);
        //EXPORT uint32_t obs_encoder_get_height(const obs_encoder_t *encoder);
        //EXPORT obs_data_t *obs_encoder_defaults(const char *id);
        //EXPORT obs_properties_t *obs_get_encoder_properties(const char *id);
        //EXPORT obs_properties_t *obs_encoder_properties(const obs_encoder_t *encoder);
        //EXPORT void obs_encoder_update(obs_encoder_t *encoder, obs_data_t *settings);
        //EXPORT bool obs_encoder_get_extra_data(const obs_encoder_t *encoder, uint8_t **extra_data, size_t *size);
        //EXPORT obs_data_t *obs_encoder_get_settings(const obs_encoder_t *encoder);
        //EXPORT void obs_encoder_set_video(obs_encoder_t *encoder, video_t *video);
        //EXPORT void obs_encoder_set_audio(obs_encoder_t *encoder, audio_t *audio);
        //EXPORT video_t *obs_encoder_video(const obs_encoder_t *encoder);
        //EXPORT audio_t *obs_encoder_audio(const obs_encoder_t *encoder);
        //EXPORT bool obs_encoder_active(const obs_encoder_t *encoder);
        //EXPORT void obs_duplicate_encoder_packet(struct encoder_packet *dst, const struct encoder_packet *src);
        //EXPORT void obs_free_encoder_packet(struct encoder_packet *packet);
    }
}