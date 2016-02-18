# ss_lockedstories
Get a list of Starlight Stage stories that are available to you but locked due to insufficient fan count.

This was created because Chihiro-sama is being very uncooperative when it comes to finding out which idols I need to raise the fan count for to unlock more commus.
Chihiro-sama is a great, beautiful and benevolent goddess and I love her voice but I will break something if I have to listen to one more "aidoru no minna to communication wo torimashou". Please forgive me, Chihiro-sama.

Requirements:  
1. Android version of Idolm@ster Cinderella Girls Starlight Stage  
2. Packet sniffer (Refer to http://stackoverflow.com/questions/27887719/capture-packets-in-android for some options. It's highly recommended to use a method that gives out a PCAP file that you can open in Wireshark, which can be downloaded at https://www.wireshark.org/download.html, as this document will assume you're using it)  
3. story_details.csv file extracted from master_story.bdb.lz4 (First release includes this from 18/2/2016 update, and newer files may or may not be provided later)  
4. Some goddamned patience  

How to:  
1. Start packet sniffer and begin capture.  
2. Start Starlight Stage.  
3. Once the home screen loads, quit Starlight Stage and stop packet sniffing.  
4. Using Wireshark, filter captured packets by HTTP to make life a bit easier.  
5. Look for a request that has "POST /load/index HTTP/1.1  (application/x-www-form-urlencoded)" in the "info" column.  
6. Select this request and double click "Hypertext Transfer Protocol" in the list below.  
7. Scroll down and look for the header called "UDID".  
8. Right click it, Copy -> Value.  
9. Extract the release, open encodedudid.txt, and clear the content inside.  
10. Paste the copied text, remove the "UDID: " part (including the space after ':') and save.  
11. Close the file and go back to Wireshark.  
12. Below "POST /load/index HTTP/1.1  (application/x-www-form-urlencoded)", look for the response from the server, "HTTP/1.1 200 OK  (application/x-msgpack)" in the "info" column.  
13. Select it and double click "Media Type" in the list below.  
14. Right click "Media Type: application/x-msgpack (_ bytes)", Export Selected Packet Bytes.  
15. Save it as "msgpackdata" (no file extension) at the same location that the release was extracted.  
16. Kneel and pray to Chihiro-sama. (No, this is not optional.)  
17. Open WindowsFormsApplication3.exe to generate a text file with the details of the stories that require more fans to unlock.  
