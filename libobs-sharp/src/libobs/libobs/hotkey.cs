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
using System.Runtime.InteropServices;

namespace OBS
{
	using uint32_t = UInt32;

	public static partial class libobs
	{
		//EXPORT obs_hotkey_id obs_hotkey_get_id(const obs_hotkey_t* key);
		//EXPORT const char* obs_hotkey_get_name(const obs_hotkey_t* key);
		//EXPORT const char* obs_hotkey_get_description(const obs_hotkey_t* key);
		//EXPORT obs_hotkey_registerer_t obs_hotkey_get_registerer_type(const obs_hotkey_t* key);
		//EXPORT void* obs_hotkey_get_registerer(const obs_hotkey_t* key);
		//EXPORT obs_hotkey_id obs_hotkey_get_pair_partner_id(const obs_hotkey_t* key);

		//EXPORT obs_key_combination_t obs_hotkey_binding_get_key_combination(obs_hotkey_binding_t* binding);
		//EXPORT obs_hotkey_id obs_hotkey_binding_get_hotkey_id(obs_hotkey_binding_t* binding);
		//EXPORT obs_hotkey_t *obs_hotkey_binding_get_hotkey(obs_hotkey_binding_t* binding);

		//EXPORT void obs_hotkeys_set_translations_s(struct obs_hotkeys_translations *translations, size_t size);
		//EXPORT void obs_hotkeys_set_audio_hotkeys_translations(const char* mute, const char* unmute, const char* push_to_mute, const char* push_to_talk);
		//EXPORT void obs_hotkeys_set_sceneitem_hotkeys_translations(const char* show, const char* hide);

		//typedef void (*obs_hotkey_func)(void* data, obs_hotkey_id id, obs_hotkey_t* hotkey, bool pressed);
		//EXPORT obs_hotkey_id obs_hotkey_register_frontend(const char* name, const char* description, obs_hotkey_func func, void* data);
		//EXPORT obs_hotkey_id obs_hotkey_register_encoder(obs_encoder_t* encoder, const char* name, const char* description, obs_hotkey_func func, void* data);
		//EXPORT obs_hotkey_id obs_hotkey_register_output(obs_output_t* output, const char* name, const char* description, obs_hotkey_func func, void* data);
		//EXPORT obs_hotkey_id obs_hotkey_register_service(obs_service_t* service, const char* name, const char* description, obs_hotkey_func func, void* data);
		//EXPORT obs_hotkey_id obs_hotkey_register_source(obs_source_t* source, const char* name, const char* description, obs_hotkey_func func, void* data);

		//typedef bool (*obs_hotkey_active_func)(void* data, obs_hotkey_pair_id id, obs_hotkey_t* hotkey, bool pressed);
		//EXPORT obs_hotkey_pair_id obs_hotkey_pair_register_frontend(const char* name0, const char* description0, const char* name1, const char* description1, obs_hotkey_active_func func0, obs_hotkey_active_func func1, void* data0, void* data1);
		//EXPORT obs_hotkey_pair_id obs_hotkey_pair_register_encoder(obs_encoder_t* encoder, const char* name0, const char* description0, const char* name1, const char* description1, obs_hotkey_active_func func0, obs_hotkey_active_func func1, void* data0, void* data1);
		//EXPORT obs_hotkey_pair_id obs_hotkey_pair_register_output(obs_output_t* output, const char* name0, const char* description0, const char* name1, const char* description1, obs_hotkey_active_func func0, obs_hotkey_active_func func1, void* data0, void* data1);
		//EXPORT obs_hotkey_pair_id obs_hotkey_pair_register_service(obs_service_t* service, const char* name0, const char* description0, const char* name1, const char* description1, obs_hotkey_active_func func0, obs_hotkey_active_func func1, void* data0, void* data1);
		//EXPORT obs_hotkey_pair_id obs_hotkey_pair_register_source(obs_source_t* source, const char* name0, const char* description0, const char* name1, const char* description1, obs_hotkey_active_func func0, obs_hotkey_active_func func1, void* data0, void* data1);

		//EXPORT void obs_hotkey_unregister(obs_hotkey_id id);
		//EXPORT void obs_hotkey_pair_unregister(obs_hotkey_pair_id id);

		//EXPORT void obs_hotkey_load_bindings(obs_hotkey_id id, obs_key_combination_t* combinations, size_t num);
		//EXPORT void obs_hotkey_load(obs_hotkey_id id, obs_data_array_t* data);
		//EXPORT void obs_hotkeys_load_encoder(obs_encoder_t* encoder, obs_data_t* hotkeys);
		//EXPORT void obs_hotkeys_load_output(obs_output_t* output, obs_data_t* hotkeys);
		//EXPORT void obs_hotkeys_load_service(obs_service_t* service, obs_data_t* hotkeys);
		//EXPORT void obs_hotkeys_load_source(obs_source_t* source, obs_data_t* hotkeys);
		//EXPORT void obs_hotkey_pair_load(obs_hotkey_pair_id id, obs_data_array_t* data0, obs_data_array_t* data1);

		//EXPORT obs_data_array_t *obs_hotkey_save(obs_hotkey_id id);
		//EXPORT obs_data_t *obs_hotkeys_save_encoder(obs_encoder_t* encoder);
		//EXPORT obs_data_t *obs_hotkeys_save_output(obs_output_t* output);
		//EXPORT obs_data_t *obs_hotkeys_save_service(obs_service_t* service);
		//EXPORT obs_data_t *obs_hotkeys_save_source(obs_source_t* source);

		//typedef bool (*obs_hotkey_enum_func)(void* data, obs_hotkey_id id, obs_hotkey_t* key);
		//EXPORT void obs_enum_hotkeys(obs_hotkey_enum_func func, void* data);

		//typedef bool (*obs_hotkey_binding_enum_func)(void* data, size_t idx, obs_hotkey_binding_t* binding);
		//EXPORT void obs_enum_hotkey_bindings(obs_hotkey_binding_enum_func func, void* data);

		//EXPORT void obs_hotkey_inject_event(obs_key_combination_t hotkey, bool pressed);
		//EXPORT void obs_hotkey_enable_background_press(bool enable);
		//EXPORT void obs_hotkey_enable_strict_modifiers(bool enable);

		//typedef void (*obs_hotkey_callback_router_func)(void* data, obs_hotkey_id id, bool pressed);
		//EXPORT void obs_hotkey_set_callback_routing_func(obs_hotkey_callback_router_func func, void* data);
		//EXPORT void obs_hotkey_trigger_routed_callback(obs_hotkey_id id, bool pressed);
		//EXPORT void obs_hotkey_enable_callback_rerouting(bool enable);

		//typedef void (*obs_hotkey_atomic_update_func)(void*);
		//EXPORT void obs_hotkey_update_atomic(obs_hotkey_atomic_update_func func, void* data);
		//EXPORT void obs_key_to_str(obs_key_t key, struct dstr *str);
		//EXPORT void obs_key_combination_to_str(obs_key_combination_t key, struct dstr *str);
		//EXPORT obs_key_t obs_key_from_virtual_key(int code);
		//EXPORT int obs_key_to_virtual_key(obs_key_t key);
		//EXPORT const char* obs_key_to_name(obs_key_t key);
		//EXPORT obs_key_t obs_key_from_name(const char* name);

		public enum obs_hotkey_registerer_t
		{
			OBS_HOTKEY_REGISTERER_FRONTEND,
			OBS_HOTKEY_REGISTERER_SOURCE,
			OBS_HOTKEY_REGISTERER_OUTPUT,
			OBS_HOTKEY_REGISTERER_ENCODER,
			OBS_HOTKEY_REGISTERER_SERVICE,
		}

		public enum obs_key_t
		{
#region obs-hotkeys.h
			OBS_KEY_NONE,

			OBS_KEY_RETURN,
			OBS_KEY_ENTER,
			OBS_KEY_ESCAPE,
			OBS_KEY_TAB,
			OBS_KEY_BACKTAB,
			OBS_KEY_BACKSPACE,
			OBS_KEY_INSERT,
			OBS_KEY_DELETE,
			OBS_KEY_PAUSE,
			OBS_KEY_PRINT,
			OBS_KEY_SYSREQ,
			OBS_KEY_CLEAR,
			OBS_KEY_HOME,
			OBS_KEY_END,
			OBS_KEY_LEFT,
			OBS_KEY_UP,
			OBS_KEY_RIGHT,
			OBS_KEY_DOWN,
			OBS_KEY_PAGEUP,
			OBS_KEY_PAGEDOWN,

			OBS_KEY_SHIFT,
			OBS_KEY_CONTROL,
			OBS_KEY_META,
			OBS_KEY_ALT,
			OBS_KEY_ALTGR,
			OBS_KEY_CAPSLOCK,
			OBS_KEY_NUMLOCK,
			OBS_KEY_SCROLLLOCK,

			OBS_KEY_F1,
			OBS_KEY_F2,
			OBS_KEY_F3,
			OBS_KEY_F4,
			OBS_KEY_F5,
			OBS_KEY_F6,
			OBS_KEY_F7,
			OBS_KEY_F8,
			OBS_KEY_F9,
			OBS_KEY_F10,
			OBS_KEY_F11,
			OBS_KEY_F12,
			OBS_KEY_F13,
			OBS_KEY_F14,
			OBS_KEY_F15,
			OBS_KEY_F16,
			OBS_KEY_F17,
			OBS_KEY_F18,
			OBS_KEY_F19,
			OBS_KEY_F20,
			OBS_KEY_F21,
			OBS_KEY_F22,
			OBS_KEY_F23,
			OBS_KEY_F24,
			OBS_KEY_F25,
			OBS_KEY_F26,
			OBS_KEY_F27,
			OBS_KEY_F28,
			OBS_KEY_F29,
			OBS_KEY_F30,
			OBS_KEY_F31,
			OBS_KEY_F32,
			OBS_KEY_F33,
			OBS_KEY_F34,
			OBS_KEY_F35,

			OBS_KEY_MENU,
			OBS_KEY_HYPER_L,
			OBS_KEY_HYPER_R,
			OBS_KEY_HELP,
			OBS_KEY_DIRECTION_L,
			OBS_KEY_DIRECTION_R,

			OBS_KEY_SPACE,
			OBS_KEY_EXCLAM,
			OBS_KEY_QUOTEDBL,
			OBS_KEY_NUMBERSIGN,
			OBS_KEY_DOLLAR,
			OBS_KEY_PERCENT,
			OBS_KEY_AMPERSAND,
			OBS_KEY_APOSTROPHE,
			OBS_KEY_PARENLEFT,
			OBS_KEY_PARENRIGHT,
			OBS_KEY_ASTERISK,
			OBS_KEY_PLUS,
			OBS_KEY_COMMA,
			OBS_KEY_MINUS,
			OBS_KEY_PERIOD,
			OBS_KEY_SLASH,
			OBS_KEY_0,
			OBS_KEY_1,
			OBS_KEY_2,
			OBS_KEY_3,
			OBS_KEY_4,
			OBS_KEY_5,
			OBS_KEY_6,
			OBS_KEY_7,
			OBS_KEY_8,
			OBS_KEY_9,
			OBS_KEY_NUMEQUAL,
			OBS_KEY_NUMASTERISK,
			OBS_KEY_NUMPLUS,
			OBS_KEY_NUMCOMMA,
			OBS_KEY_NUMMINUS,
			OBS_KEY_NUMPERIOD,
			OBS_KEY_NUMSLASH,
			OBS_KEY_NUM0,
			OBS_KEY_NUM1,
			OBS_KEY_NUM2,
			OBS_KEY_NUM3,
			OBS_KEY_NUM4,
			OBS_KEY_NUM5,
			OBS_KEY_NUM6,
			OBS_KEY_NUM7,
			OBS_KEY_NUM8,
			OBS_KEY_NUM9,
			OBS_KEY_COLON,
			OBS_KEY_SEMICOLON,
			OBS_KEY_QUOTE,
			OBS_KEY_LESS,
			OBS_KEY_EQUAL,
			OBS_KEY_GREATER,
			OBS_KEY_QUESTION,
			OBS_KEY_AT,
			OBS_KEY_A,
			OBS_KEY_B,
			OBS_KEY_C,
			OBS_KEY_D,
			OBS_KEY_E,
			OBS_KEY_F,
			OBS_KEY_G,
			OBS_KEY_H,
			OBS_KEY_I,
			OBS_KEY_J,
			OBS_KEY_K,
			OBS_KEY_L,
			OBS_KEY_M,
			OBS_KEY_N,
			OBS_KEY_O,
			OBS_KEY_P,
			OBS_KEY_Q,
			OBS_KEY_R,
			OBS_KEY_S,
			OBS_KEY_T,
			OBS_KEY_U,
			OBS_KEY_V,
			OBS_KEY_W,
			OBS_KEY_X,
			OBS_KEY_Y,
			OBS_KEY_Z,
			OBS_KEY_BRACKETLEFT,
			OBS_KEY_BACKSLASH,
			OBS_KEY_BRACKETRIGHT,
			OBS_KEY_ASCIICIRCUM,
			OBS_KEY_UNDERSCORE,
			OBS_KEY_QUOTELEFT,
			OBS_KEY_BRACELEFT,
			OBS_KEY_BAR,
			OBS_KEY_BRACERIGHT,
			OBS_KEY_ASCIITILDE,
			OBS_KEY_NOBREAKSPACE,
			OBS_KEY_EXCLAMDOWN,
			OBS_KEY_CENT,
			OBS_KEY_STERLING,
			OBS_KEY_CURRENCY,
			OBS_KEY_YEN,
			OBS_KEY_BROKENBAR,
			OBS_KEY_SECTION,
			OBS_KEY_DIAERESIS,
			OBS_KEY_COPYRIGHT,
			OBS_KEY_ORDFEMININE,
			OBS_KEY_GUILLEMOTLEFT,
			OBS_KEY_NOTSIGN,
			OBS_KEY_HYPHEN,
			OBS_KEY_REGISTERED,
			OBS_KEY_MACRON,
			OBS_KEY_DEGREE,
			OBS_KEY_PLUSMINUS,
			OBS_KEY_TWOSUPERIOR,
			OBS_KEY_THREESUPERIOR,
			OBS_KEY_ACUTE,
			OBS_KEY_MU,
			OBS_KEY_PARAGRAPH,
			OBS_KEY_PERIODCENTERED,
			OBS_KEY_CEDILLA,
			OBS_KEY_ONESUPERIOR,
			OBS_KEY_MASCULINE,
			OBS_KEY_GUILLEMOTRIGHT,
			OBS_KEY_ONEQUARTER,
			OBS_KEY_ONEHALF,
			OBS_KEY_THREEQUARTERS,
			OBS_KEY_QUESTIONDOWN,
			OBS_KEY_AGRAVE,
			OBS_KEY_AACUTE,
			OBS_KEY_ACIRCUMFLEX,
			OBS_KEY_ATILDE,
			OBS_KEY_ADIAERESIS,
			OBS_KEY_ARING,
			OBS_KEY_AE,
			OBS_KEY_CCEDILLA,
			OBS_KEY_EGRAVE,
			OBS_KEY_EACUTE,
			OBS_KEY_ECIRCUMFLEX,
			OBS_KEY_EDIAERESIS,
			OBS_KEY_IGRAVE,
			OBS_KEY_IACUTE,
			OBS_KEY_ICIRCUMFLEX,
			OBS_KEY_IDIAERESIS,
			OBS_KEY_ETH,
			OBS_KEY_NTILDE,
			OBS_KEY_OGRAVE,
			OBS_KEY_OACUTE,
			OBS_KEY_OCIRCUMFLEX,
			OBS_KEY_OTILDE,
			OBS_KEY_ODIAERESIS,
			OBS_KEY_MULTIPLY,
			OBS_KEY_OOBLIQUE,
			OBS_KEY_UGRAVE,
			OBS_KEY_UACUTE,
			OBS_KEY_UCIRCUMFLEX,
			OBS_KEY_UDIAERESIS,
			OBS_KEY_YACUTE,
			OBS_KEY_THORN,
			OBS_KEY_SSHARP,
			OBS_KEY_DIVISION,
			OBS_KEY_YDIAERESIS,
			OBS_KEY_MULTI_KEY,
			OBS_KEY_CODEINPUT,
			OBS_KEY_SINGLECANDIDATE,
			OBS_KEY_MULTIPLECANDIDATE,
			OBS_KEY_PREVIOUSCANDIDATE,
			OBS_KEY_MODE_SWITCH,
			OBS_KEY_KANJI,
			OBS_KEY_MUHENKAN,
			OBS_KEY_HENKAN,
			OBS_KEY_ROMAJI,
			OBS_KEY_HIRAGANA,
			OBS_KEY_KATAKANA,
			OBS_KEY_HIRAGANA_KATAKANA,
			OBS_KEY_ZENKAKU,
			OBS_KEY_HANKAKU,
			OBS_KEY_ZENKAKU_HANKAKU,
			OBS_KEY_TOUROKU,
			OBS_KEY_MASSYO,
			OBS_KEY_KANA_LOCK,
			OBS_KEY_KANA_SHIFT,
			OBS_KEY_EISU_SHIFT,
			OBS_KEY_EISU_TOGGLE,
			OBS_KEY_HANGUL,
			OBS_KEY_HANGUL_START,
			OBS_KEY_HANGUL_END,
			OBS_KEY_HANGUL_HANJA,
			OBS_KEY_HANGUL_JAMO,
			OBS_KEY_HANGUL_ROMAJA,
			OBS_KEY_HANGUL_JEONJA,
			OBS_KEY_HANGUL_BANJA,
			OBS_KEY_HANGUL_PREHANJA,
			OBS_KEY_HANGUL_POSTHANJA,
			OBS_KEY_HANGUL_SPECIAL,
			OBS_KEY_DEAD_GRAVE,
			OBS_KEY_DEAD_ACUTE,
			OBS_KEY_DEAD_CIRCUMFLEX,
			OBS_KEY_DEAD_TILDE,
			OBS_KEY_DEAD_MACRON,
			OBS_KEY_DEAD_BREVE,
			OBS_KEY_DEAD_ABOVEDOT,
			OBS_KEY_DEAD_DIAERESIS,
			OBS_KEY_DEAD_ABOVERING,
			OBS_KEY_DEAD_DOUBLEACUTE,
			OBS_KEY_DEAD_CARON,
			OBS_KEY_DEAD_CEDILLA,
			OBS_KEY_DEAD_OGONEK,
			OBS_KEY_DEAD_IOTA,
			OBS_KEY_DEAD_VOICED_SOUND,
			OBS_KEY_DEAD_SEMIVOICED_SOUND,
			OBS_KEY_DEAD_BELOWDOT,
			OBS_KEY_DEAD_HOOK,
			OBS_KEY_DEAD_HORN,
			OBS_KEY_BACK,
			OBS_KEY_FORWARD,
			OBS_KEY_STOP,
			OBS_KEY_REFRESH,
			OBS_KEY_VOLUMEDOWN,
			OBS_KEY_VOLUMEMUTE,
			OBS_KEY_VOLUMEUP,
			OBS_KEY_BASSBOOST,
			OBS_KEY_BASSUP,
			OBS_KEY_BASSDOWN,
			OBS_KEY_TREBLEUP,
			OBS_KEY_TREBLEDOWN,
			OBS_KEY_MEDIAPLAY,
			OBS_KEY_MEDIASTOP,
			OBS_KEY_MEDIAPREVIOUS,
			OBS_KEY_MEDIANEXT,
			OBS_KEY_MEDIARECORD,
			OBS_KEY_MEDIAPAUSE,
			OBS_KEY_MEDIATOGGLEPLAYPAUSE,
			OBS_KEY_HOMEPAGE,
			OBS_KEY_FAVORITES,
			OBS_KEY_SEARCH,
			OBS_KEY_STANDBY,
			OBS_KEY_OPENURL,
			OBS_KEY_LAUNCHMAIL,
			OBS_KEY_LAUNCHMEDIA,
			OBS_KEY_LAUNCH0,
			OBS_KEY_LAUNCH1,
			OBS_KEY_LAUNCH2,
			OBS_KEY_LAUNCH3,
			OBS_KEY_LAUNCH4,
			OBS_KEY_LAUNCH5,
			OBS_KEY_LAUNCH6,
			OBS_KEY_LAUNCH7,
			OBS_KEY_LAUNCH8,
			OBS_KEY_LAUNCH9,
			OBS_KEY_LAUNCHA,
			OBS_KEY_LAUNCHB,
			OBS_KEY_LAUNCHC,
			OBS_KEY_LAUNCHD,
			OBS_KEY_LAUNCHE,
			OBS_KEY_LAUNCHF,
			OBS_KEY_LAUNCHG,
			OBS_KEY_LAUNCHH,
			OBS_KEY_MONBRIGHTNESSUP,
			OBS_KEY_MONBRIGHTNESSDOWN,
			OBS_KEY_KEYBOARDLIGHTONOFF,
			OBS_KEY_KEYBOARDBRIGHTNESSUP,
			OBS_KEY_KEYBOARDBRIGHTNESSDOWN,
			OBS_KEY_POWEROFF,
			OBS_KEY_WAKEUP,
			OBS_KEY_EJECT,
			OBS_KEY_SCREENSAVER,
			OBS_KEY_WWW,
			OBS_KEY_MEMO,
			OBS_KEY_LIGHTBULB,
			OBS_KEY_SHOP,
			OBS_KEY_HISTORY,
			OBS_KEY_ADDFAVORITE,
			OBS_KEY_HOTLINKS,
			OBS_KEY_BRIGHTNESSADJUST,
			OBS_KEY_FINANCE,
			OBS_KEY_COMMUNITY,
			OBS_KEY_AUDIOREWIND,
			OBS_KEY_BACKFORWARD,
			OBS_KEY_APPLICATIONLEFT,
			OBS_KEY_APPLICATIONRIGHT,
			OBS_KEY_BOOK,
			OBS_KEY_CD,
			OBS_KEY_CALCULATOR,
			OBS_KEY_TODOLIST,
			OBS_KEY_CLEARGRAB,
			OBS_KEY_CLOSE,
			OBS_KEY_COPY,
			OBS_KEY_CUT,
			OBS_KEY_DISPLAY,
			OBS_KEY_DOS,
			OBS_KEY_DOCUMENTS,
			OBS_KEY_EXCEL,
			OBS_KEY_EXPLORER,
			OBS_KEY_GAME,
			OBS_KEY_GO,
			OBS_KEY_ITOUCH,
			OBS_KEY_LOGOFF,
			OBS_KEY_MARKET,
			OBS_KEY_MEETING,
			OBS_KEY_MENUKB,
			OBS_KEY_MENUPB,
			OBS_KEY_MYSITES,
			OBS_KEY_NEWS,
			OBS_KEY_OFFICEHOME,
			OBS_KEY_OPTION,
			OBS_KEY_PASTE,
			OBS_KEY_PHONE,
			OBS_KEY_CALENDAR,
			OBS_KEY_REPLY,
			OBS_KEY_RELOAD,
			OBS_KEY_ROTATEWINDOWS,
			OBS_KEY_ROTATIONPB,
			OBS_KEY_ROTATIONKB,
			OBS_KEY_SAVE,
			OBS_KEY_SEND,
			OBS_KEY_SPELL,
			OBS_KEY_SPLITSCREEN,
			OBS_KEY_SUPPORT,
			OBS_KEY_TASKPANE,
			OBS_KEY_TERMINAL,
			OBS_KEY_TOOLS,
			OBS_KEY_TRAVEL,
			OBS_KEY_VIDEO,
			OBS_KEY_WORD,
			OBS_KEY_XFER,
			OBS_KEY_ZOOMIN,
			OBS_KEY_ZOOMOUT,
			OBS_KEY_AWAY,
			OBS_KEY_MESSENGER,
			OBS_KEY_WEBCAM,
			OBS_KEY_MAILFORWARD,
			OBS_KEY_PICTURES,
			OBS_KEY_MUSIC,
			OBS_KEY_BATTERY,
			OBS_KEY_BLUETOOTH,
			OBS_KEY_WLAN,
			OBS_KEY_UWB,
			OBS_KEY_AUDIOFORWARD,
			OBS_KEY_AUDIOREPEAT,
			OBS_KEY_AUDIORANDOMPLAY,
			OBS_KEY_SUBTITLE,
			OBS_KEY_AUDIOCYCLETRACK,
			OBS_KEY_TIME,
			OBS_KEY_HIBERNATE,
			OBS_KEY_VIEW,
			OBS_KEY_TOPMENU,
			OBS_KEY_POWERDOWN,
			OBS_KEY_SUSPEND,
			OBS_KEY_CONTRASTADJUST,
			OBS_KEY_MEDIALAST,
			OBS_KEY_CALL,
			OBS_KEY_CAMERA,
			OBS_KEY_CAMERAFOCUS,
			OBS_KEY_CONTEXT1,
			OBS_KEY_CONTEXT2,
			OBS_KEY_CONTEXT3,
			OBS_KEY_CONTEXT4,
			OBS_KEY_FLIP,
			OBS_KEY_HANGUP,
			OBS_KEY_NO,
			OBS_KEY_SELECT,
			OBS_KEY_YES,
			OBS_KEY_TOGGLECALLHANGUP,
			OBS_KEY_VOICEDIAL,
			OBS_KEY_LASTNUMBERREDIAL,
			OBS_KEY_EXECUTE,
			OBS_KEY_PRINTER,
			OBS_KEY_PLAY,
			OBS_KEY_SLEEP,
			OBS_KEY_ZOOM,
			OBS_KEY_CANCEL,

			OBS_KEY_MOUSE1,
			OBS_KEY_MOUSE2,
			OBS_KEY_MOUSE3,
			OBS_KEY_MOUSE4,
			OBS_KEY_MOUSE5,
			OBS_KEY_MOUSE6,
			OBS_KEY_MOUSE7,
			OBS_KEY_MOUSE8,
			OBS_KEY_MOUSE9,
			OBS_KEY_MOUSE10,
			OBS_KEY_MOUSE11,
			OBS_KEY_MOUSE12,
			OBS_KEY_MOUSE13,
			OBS_KEY_MOUSE14,
			OBS_KEY_MOUSE15,
			OBS_KEY_MOUSE16,
			OBS_KEY_MOUSE17,
			OBS_KEY_MOUSE18,
			OBS_KEY_MOUSE19,
			OBS_KEY_MOUSE20,
			OBS_KEY_MOUSE21,
			OBS_KEY_MOUSE22,
			OBS_KEY_MOUSE23,
			OBS_KEY_MOUSE24,
			OBS_KEY_MOUSE25,
			OBS_KEY_MOUSE26,
			OBS_KEY_MOUSE27,
			OBS_KEY_MOUSE28,
			OBS_KEY_MOUSE29,
#endregion
			OBS_KEY_LAST_VALUE,
		}

		[StructLayout(LayoutKind.Explicit, Size = 8)]
		public struct obs_key_combination
		{
			[FieldOffset(0)]
			public uint32_t modifiers;

			[FieldOffset(0)]
			public obs_key_t key;
		}
	}
}
