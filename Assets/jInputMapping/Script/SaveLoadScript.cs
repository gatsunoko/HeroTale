using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Security.Cryptography;

public class SaveLoadScript : MonoBehaviour
{
	string InputSettingSaveFilePath = @"SaveData/InputMapping.dat";
	string sKy = "lkirwf897+22#bbtrm8814z5qq=498j5"; //'32 chr shared ascii string (32 * 8 = 256 bit)
	string sIV = "741952hheeyy66#cs!9hjv887mxx7@8y";  //'32 chr shared ascii string (32 * 8 = 256 bit)
	FileStream BinaryFile;
	Mapper MappingScript;

	public void SaveInputSetting ()
	{
		if (MappingScript == null) {
			if (MappingScript = GameObject.Find ("jInputMappingManager").GetComponent<Mapper> ()) {
				
			} else {
				Debug.LogError ("[jInput] jInputMappingManager is Not Found!!");
			}
		}
		try {
			string InputSettingSaveData = MappingScript.MapperSaveData;
			Directory.CreateDirectory ("SaveData");
			BinaryFile = new FileStream (InputSettingSaveFilePath, FileMode.Create, FileAccess.ReadWrite);
			BinaryWriter Writer = new BinaryWriter (BinaryFile);
			
			string EncryptData = EncryptRJ256 (sKy, sIV, InputSettingSaveData);
			Writer.Write (EncryptData);
			//Writer.Write(InputSettingSaveData); //do not encrypt
			
			Debug.Log ("[jInput] Input Mapping Data Save Success!");
			if (MappingScript != null) {
				MappingScript.SaveSucsessIndicate ();
			}
		} catch (IOException) {
			Debug.LogError ("[jInput] Input Mapping Data Save failure!");
			if (MappingScript != null) {
				MappingScript.SavefailureIndicate ();
			}
		} finally {
			if (BinaryFile != null) {
				BinaryFile.Close ();
				BinaryFile.Dispose ();
			}
		}
	}
	
	public void LoadInputSetting ()
	{
		if (File.Exists (InputSettingSaveFilePath)) {
			if (MappingScript == null) {
				if (MappingScript = GameObject.Find ("jInputMappingManager").GetComponent<Mapper> ()) {
					
				} else {
					Debug.LogError ("[jInput] jInputMappingManager is Not Found!!");
				}
			}
			try {
				BinaryFile = new FileStream (InputSettingSaveFilePath, FileMode.Open);
				BinaryReader Reader = new BinaryReader (BinaryFile);
				BinaryFile.Seek (0, SeekOrigin.Begin);
				string ReadData = Reader.ReadString (); //stringType
				
				MappingScript.MapperSaveData = DecryptRJ256 (sKy, sIV, ReadData);
				//MappingScript.MapperSaveData = ReadData; //do not encrypt
				
				Debug.Log ("[jInput] Input Mapping Data Loaded.");
			} catch (IOException) {
				Debug.LogError ("[jInput] Input Mapping Data Load failure!");
				if (MappingScript != null) {
					MappingScript.LoadfailureDeal ();
					MappingScript.SaveFileDelete();
				}
			} finally {
				if (BinaryFile != null) {
					BinaryFile.Close ();
					BinaryFile.Dispose ();
				}
			}
		} else {
			SaveInputSetting ();
		}
	}

	public void DeleteInputSetting ()
	{
		if (File.Exists (InputSettingSaveFilePath)) {
			try {
				File.Delete (InputSettingSaveFilePath);
			} catch (IOException) {
				BinaryFile.Close ();
				BinaryFile.Dispose ();
				File.Delete (InputSettingSaveFilePath);
			}
		}
	}

	public string EncryptRJ256 (string prm_key, string prm_iv, string prm_text_to_encrypt)
	{
		string sToEncrypt = prm_text_to_encrypt;

		RijndaelManaged myRijndael = new RijndaelManaged ();
		myRijndael.Padding = PaddingMode.Zeros;
		myRijndael.Mode = CipherMode.CBC;
		myRijndael.KeySize = 256;
		myRijndael.BlockSize = 256;
		
		byte[] key = System.Text.Encoding.UTF8.GetBytes (prm_key);
		byte[] IV = System.Text.Encoding.UTF8.GetBytes (prm_iv);
		
		ICryptoTransform encryptor = myRijndael.CreateEncryptor (key, IV);
		
		MemoryStream msEncrypt = new MemoryStream ();
		CryptoStream csEncrypt = new CryptoStream (msEncrypt, encryptor, CryptoStreamMode.Write);


		byte[] toEncrypt = System.Text.Encoding.UTF8.GetBytes (sToEncrypt);

		csEncrypt.Write (toEncrypt, 0, toEncrypt.Length);
		csEncrypt.FlushFinalBlock ();
		byte[] encrypted = msEncrypt.ToArray ();

		var EncryptOut = Convert.ToBase64String (encrypted);

		return (EncryptOut);
	}
	
	public string DecryptRJ256 (string prm_key, string prm_iv, string prm_text_to_decrypt)
	{
		string sEncryptedString = prm_text_to_decrypt;

		RijndaelManaged myRijndael = new RijndaelManaged ();
		myRijndael.Padding = PaddingMode.Zeros;
		myRijndael.Mode = CipherMode.CBC;
		myRijndael.KeySize = 256;
		myRijndael.BlockSize = 256;

		byte[] key = System.Text.Encoding.UTF8.GetBytes (prm_key);
		byte[] IV = System.Text.Encoding.UTF8.GetBytes (prm_iv);
		
		ICryptoTransform decryptor = myRijndael.CreateDecryptor (key, IV);
		
		byte[] sEncrypted = Convert.FromBase64String (sEncryptedString);
		byte[] fromEncrypt = new byte[sEncrypted.Length];

		MemoryStream msDecrypt = new MemoryStream (sEncrypted);
		CryptoStream csDecrypt = new CryptoStream (msDecrypt, decryptor, CryptoStreamMode.Read);
		
		csDecrypt.Read (fromEncrypt, 0, fromEncrypt.Length);

		var DecryptOut = System.Text.Encoding.UTF8.GetString (fromEncrypt);

		return (DecryptOut);
	}

}
