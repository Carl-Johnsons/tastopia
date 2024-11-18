import React, { useState, useEffect, useRef } from "react";
import { View, Button, Text } from "react-native";
import * as Speech from "expo-speech";
import { Audio } from "expo-av";
import { TouchableHighlight } from "react-native-gesture-handler";
import { SafeAreaView } from "react-native-safe-area-context";

const SILENCE_THRESHOLD = -26;
const SILENCE_DURATION = 2500;
const MONITORING_INTERVAL = 500;
const paragraphs = [
  "This is the first paragraph.",
  "Here's the second paragraph.",
  "And finally, the third paragraph."
];

const Home = () => {
  const [isListening, setIsListening] = useState(false);
  const [currentParagraphIndex, setCurrentParagraphIndex] = useState(0);

  const recording = useRef<Audio.Recording | null>(null);
  const silenceStartTime = useRef<number | null>(null);
  const hasDetectedVoice = useRef(false);

  useEffect(() => {
    return () => {
      if (recording) {
        recording.current?.stopAndUnloadAsync();
      }
    };
  }, []);

  const startListening = async () => {
    try {
      await Audio.requestPermissionsAsync();
      await Audio.setAudioModeAsync({
        allowsRecordingIOS: true,
        playsInSilentModeIOS: true
      });

      const { recording: newRecording } = await Audio.Recording.createAsync(
        Audio.RecordingOptionsPresets.HIGH_QUALITY,
        onRecordingStatusUpdate,
        MONITORING_INTERVAL
      );
      console.log("recording", recording);
      recording.current = newRecording;
      setIsListening(true);
      hasDetectedVoice.current = false;
      silenceStartTime.current = null;
    } catch (err) {
      console.error("Failed to start recording", err);
    }
  };

  const onRecordingStatusUpdate = (status: Audio.RecordingStatus) => {
    if (!status.isRecording) return;

    const metering = status.metering ?? -160;
    const currentTime = Date.now();

    console.log("Metering:", metering);

    if (metering > SILENCE_THRESHOLD) {
      /** Detected voice */
      hasDetectedVoice.current = true;
      silenceStartTime.current = null;
      console.log("Detected voice");
    } else if (hasDetectedVoice.current) {
      /** Detected silence & start timer */
      if (silenceStartTime.current === null) {
        console.log("Starting silence timer");
        silenceStartTime.current = currentTime;
      } else {
        /** Detected silence & timer already started */
        const silenceDuration = currentTime - silenceStartTime.current;
        console.log("Silence timer running", silenceDuration);
        /** If silence long enough then send API */
        if (silenceDuration >= SILENCE_DURATION) {
          console.log("2 seconds of silence detected after speech");
          stopListening();
        }
      }
    }
  };

  const stopListening = async () => {
    console.log("Stopping recording...", recording);

    if (!recording) return;

    try {
      setIsListening(false);
      await recording.current?.stopAndUnloadAsync();
      const uri = recording.current?.getURI();

      console.log("uri", uri);
      console.log("hasDetectedVoice", hasDetectedVoice.current);

      if (uri && hasDetectedVoice.current) {
        // await sendAudioToGoogleAPI(uri);
        console.log("Sending audio to Google API...");
        silenceStartTime.current = null;
        hasDetectedVoice.current = false;
        recording.current = null;
        startListening();
      }
    } catch (error) {
      console.error("Error stopping recording:", error);
    }
  };

  const sendAudioToGoogleAPI = async (uri: string) => {
    try {
      const formData = new FormData();
      formData.append("audio", {
        uri,
        type: "audio/m4a",
        name: "speech.m4a"
      } as any);

      const response = await fetch("YOUR_GOOGLE_SPEECH_API_ENDPOINT", {
        method: "POST",
        body: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: "Bearer YOUR_API_KEY"
        }
      });

      const result = await response.json();
      console.log("Transcription:", result.text);
      processVoiceCommand(result.text);
    } catch (error) {
      console.error("Error sending audio to Google API:", error);
    }
  };

  const processVoiceCommand = (command: string) => {
    if (command.toLowerCase() === "next") {
      readNextParagraph();
    }
  };

  const readNextParagraph = () => {
    if (currentParagraphIndex < paragraphs.length) {
      Speech.speak(paragraphs[currentParagraphIndex], {
        onDone: () => {
          setCurrentParagraphIndex(prev => prev + 1);
        }
      });
    }
  };

  return (
    <SafeAreaView style={{ padding: 20 }}>
      <TouchableHighlight onPress={isListening ? stopListening : startListening}>
        <Text>{isListening ? "Stop Listening" : "Start Listening"}</Text>
      </TouchableHighlight>

      {paragraphs.map((paragraph, index) => (
        <Text
          key={index}
          style={{
            marginVertical: 10,
            fontWeight: currentParagraphIndex === index ? "bold" : "normal"
          }}
        >
          {paragraph}
        </Text>
      ))}
    </SafeAreaView>
  );
};

export default Home;
