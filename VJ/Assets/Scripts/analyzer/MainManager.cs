using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.IO;
using System.Linq;
using System.IO.Ports;

namespace TCHackathon
{
	public class MainManager : MonoBehaviour
	{
		public VJSequencer vJSequencer;
		public AudioManager audioManager;
		private const string GRACENOTE_API_URL = "http://devapi.gracenote.com/v1/timeline/";
		// ATTENTION! Machine- and environment- dependent.
		private const string SERIAL_PORT_PATH = "/dev/tty.usbmodem1412";
		private float currentTime = 0.0f;
		string currentMood = string.Empty;
		private SerialPort serialPort;
		private int nextBeat = 0;

		public double[] BeatTimings { get; set; }

		private string[] Moods { get; set; }

		private string AudioPath = string.Empty;

		void Start ()
		{

			if (File.Exists (SERIAL_PORT_PATH)) {
				serialPort = new SerialPort (SERIAL_PORT_PATH);
				if (serialPort != null) {
					serialPort.Open ();
				}
			}

			AudioPath = Application.dataPath + "/Resources/Sounds/test_sound.mp3";
			AnalyzeSong (AudioPath);
			audioManager.audioSource.Play ();
		}

		void Update ()
		{
			currentTime += Time.deltaTime;
			if (currentTime > 1.0f) {
				currentTime = 0.0f;
				int audioTime = (int)(UnityEngine.Mathf.Clamp (audioManager.audioTime, 0.0f, (float)Moods.Length));
				vJSequencer.ChangeMood (Moods [audioTime]);
			}

			if (nextBeat < BeatTimings.Length && audioManager.audioTime > BeatTimings [nextBeat]) {
				if (serialPort != null) {
					serialPort.Write ("B");
				}
				nextBeat++;
			}
		}

		void Dispose ()
		{
			if (serialPort != null && serialPort.IsOpen) {
				serialPort.Close ();
			}
		}

		public void AnalyzeSong (string path)
		{
			var boundary = System.Environment.TickCount.ToString ();
			var req = WebRequest.Create (GRACENOTE_API_URL);
			req.Method = WebRequestMethods.Http.Post;
			req.ContentType = String.Format ("multipart/form-data; boundary={0}", boundary);
			
			var post = String.Format ("--{0}\r\n" +
			           "Content-Disposition: form-data; name=\"audiofile\"; filename=\"{1}\"\r\n" +
			           "Content-Type: application/octet-stream\r\n\r\n",
				           boundary,
				           path);
			var startData = System.Text.Encoding.UTF8.GetBytes (post);

			post = String.Format ("\r\n--{0}--\r\n", boundary);
			var endData = System.Text.Encoding.UTF8.GetBytes (post);

			using (var fs = new FileStream (path, FileMode.Open, FileAccess.Read)) {
				req.ContentLength = startData.Length + endData.Length + fs.Length;
				using (var rs = req.GetRequestStream ()) {
					rs.Write (startData, 0, startData.Length);

					var data = new byte[fs.Length];
					fs.Read (data, 0, (int)fs.Length);
					rs.Write (data, 0, (int)fs.Length);

					rs.Write (endData, 0, endData.Length);
				}
			}

			var resp = req.GetResponse ();
			using (var sr = new StreamReader (resp.GetResponseStream ())) {
				var s = sr.ReadToEnd ();
				var json = (IDictionary)Json.Deserialize (s);
				var features = (IDictionary)json ["features"];
				var beats = (IList)features ["BEATS"];
				var moods = (IList)features ["MOODS"];
				
				BeatTimings = beats.Cast<double> ().ToArray ();
				Moods = moods
					.Cast<IDictionary> ()
					.Select (m => (IDictionary)m ["TYPE"])
					.Cast<IDictionary<string, object>> ()
					.Select (m => m.Aggregate ((l, r) => (long)(l.Value) > (long)(r.Value) ? l : r).Key)
					.ToArray ();
			}
		}
	}
}
