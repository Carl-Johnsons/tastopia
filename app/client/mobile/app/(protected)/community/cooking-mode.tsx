import { useRecipeSteps } from "@/api/recipe";
import BackButton from "@/components/BackButton";
import { globalStyles } from "@/components/common/GlobalStyles";
import Slider from "@/components/screen/community/Slider";
import Step from "@/components/screen/community/Step";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { MaterialCommunityIcons, MaterialIcons } from "@expo/vector-icons";
import { router, useLocalSearchParams } from "expo-router";
import { useEffect, useMemo, useRef, useState } from "react";
import { useTranslation } from "react-i18next";
import {
  ActivityIndicator,
  Text,
  TouchableWithoutFeedback,
  View,
  SafeAreaView,
  Switch,
  Platform,
  FlatList,
  Alert,
  StatusBar
} from "react-native";
import * as Speech from "expo-speech";
import i18n from "@/i18n/i18next";
import { EN_VOICE, VI_VOICE } from "@/constants/voice";
import { LANGUAGE_CODES } from "@/constants/languages";
import languagedetect from "languagedetect";
import { compareLanguages } from "@/utils/language";
import {
  ExpoSpeechRecognitionModule,
  useSpeechRecognitionEvent
} from "expo-speech-recognition";

const REPEAT_AFTER = 10000;

const CookingMode = () => {
  const { t } = useTranslation("cookingMode");
  const { id } = useLocalSearchParams<{ id: string }>();
  const { data, isLoading, isRefetching } = useRecipeSteps(id);

  const isActiveSpeakingRef = useRef(false);
  const timeoutRef = useRef<NodeJS.Timeout | null>(null);
  const [selectedLanguage, setSelectedLanguage] = useState<LANGUAGE_CODES>();
  const [isActiveSpeaking, setIsActiveSpeaking] = useState(false);
  const [isActiveVoiceCommand, setIsActiveVoiceCommand] = useState(false);
  const [transcript, setTranscript] = useState("");
  const [currentStep, setCurrentStep] = useState<number>(1);
  const [totalSpeak, setTotalSpeak] = useState<number>(0);

  const sortedSteps = useMemo(() => {
    return data?.sort((a, b) => a.ordinalNumber - b.ordinalNumber);
  }, [data]);

  const { primary, gray, white, black } = colors;
  const { c } = useColorizer();

  const thumbColor = c(
    Platform.select({
      ios: white.DEFAULT,
      android: black.DEFAULT
    }),
    Platform.select({
      ios: black.DEFAULT,
      android: white.DEFAULT
    })
  );

  const inactiveTrackColor = c(
    Platform.select({
      ios: gray[200],
      android: gray[200]
    }),
    Platform.select({
      ios: black.DEFAULT,
      android: white.DEFAULT
    })
  );

  const handleToggleSpeaking = () => {
    const detectLanguage = new languagedetect();
    const stepLanguage = detectLanguage.detect(
      (sortedSteps && sortedSteps[currentStep - 1]?.content) ?? ""
    )[0];
    const isSameLanguage = compareLanguages(i18n.languages[0], stepLanguage[0]);

    if (!selectedLanguage && !isSameLanguage) {
      Alert.alert(t("titleChangeLanguage"), t("descriptionChangeLanguage"), [
        {
          text: t("english"),
          onPress: () => {
            setSelectedLanguage(LANGUAGE_CODES.EN);
            setIsActiveSpeaking(prev => !prev);
          }
        },
        {
          text: t("vietnamese"),
          onPress: () => {
            setSelectedLanguage(LANGUAGE_CODES.VI);
            setIsActiveSpeaking(prev => !prev);
          }
        }
      ]);
    } else {
      setIsActiveSpeaking(prev => !prev);
    }
  };

  /** Command by voice */
  useSpeechRecognitionEvent("start", () => setIsActiveVoiceCommand(true));
  useSpeechRecognitionEvent("end", () => setIsActiveVoiceCommand(false));
  useSpeechRecognitionEvent("result", event => {
    setTranscript(event.results[0]?.transcript);
  });
  useSpeechRecognitionEvent("error", event => {
    console.log("error code:", event.error, "error message:", event.message);
  });

  const handleStartCommandVoice = async () => {
    const result = await ExpoSpeechRecognitionModule.requestPermissionsAsync();
    if (!result.granted) {
      console.warn("Permissions not granted", result);
      return;
    }

    ExpoSpeechRecognitionModule.start({
      lang: i18n.languages[0] === LANGUAGE_CODES.EN ? "en-US" : "vi-VN",
      interimResults: true,
      maxAlternatives: 1000,
      continuous: true,
      requiresOnDeviceRecognition: false,
      addsPunctuation: false,
      contextualStrings: [
        t("next").toLowerCase(),
        t("back").toLowerCase(),
        t("stop").toLowerCase()
      ]
    });
  };

  const handleToggleVoiceCommand = () => {
    if (isActiveVoiceCommand) {
      setIsActiveVoiceCommand(prev => !prev);
      ExpoSpeechRecognitionModule.stop();
    } else {
      Alert.alert(t("commandVoiceTitle"), t("commandVoiceMessage"), [
        {
          text: t("cancel"),
          onPress: () => {
            ExpoSpeechRecognitionModule.stop();
            setIsActiveVoiceCommand(false);
          }
        },
        {
          text: t("ok"),
          onPress: () => {
            handleStartCommandVoice();
            setIsActiveVoiceCommand(true);
          }
        }
      ]);
    }
  };

  const handleBack = async () => {
    await Speech.stop();
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
      timeoutRef.current = null;
    }

    setCurrentStep(prev => (prev === 1 ? data?.length || 1 : prev - 1));
  };

  const handleNext = async () => {
    await Speech.stop();
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
      timeoutRef.current = null;
    }

    setCurrentStep(prev => (prev === data?.length ? 1 : prev + 1));
  };

  const sliderData =
    sortedSteps
      ?.find(item => item.ordinalNumber === currentStep)
      ?.attachedImageUrls?.filter(item => item) || [];

  useEffect(() => {
    const isMounted = true;

    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
      timeoutRef.current = null;
    }
    Speech.stop();

    isActiveSpeakingRef.current = isActiveSpeaking;

    const speakStep = async (step: number) => {
      try {
        await Speech.stop();

        if (!isActiveSpeakingRef.current || !sortedSteps?.length || !isMounted) return;

        const stepToSpeak =
          step.toString() + " " + (sortedSteps[step - 1]?.content ?? "");

        Speech.speak(stepToSpeak, {
          voice:
            selectedLanguage === LANGUAGE_CODES.VI
              ? VI_VOICE.identifier
              : EN_VOICE.identifier,
          rate: 0.7,
          onDone: () => {
            if (isActiveSpeakingRef.current && isMounted) {
              if (timeoutRef.current) {
                clearTimeout(timeoutRef.current);
                timeoutRef.current = null;
              }

              timeoutRef.current = setTimeout(() => {
                setTotalSpeak(prevStep =>
                  prevStep >= sortedSteps.length ? 1 : prevStep + 1
                );
              }, REPEAT_AFTER);
            }
          }
        });
      } catch (error) {
        console.error("Speech error:", error);
      }
    };

    if (isActiveSpeaking && sortedSteps?.length) {
      speakStep(currentStep);
    }

    return () => {
      const cleanup = async () => {
        if (timeoutRef.current) {
          clearTimeout(timeoutRef.current);
          timeoutRef.current = null;
        }
        await Speech.stop();
      };
      cleanup();
    };
  }, [isActiveSpeaking, currentStep, sortedSteps, totalSpeak]);

  useEffect(() => {
    const handleVoiceCommands = async () => {
      console.log("transcript", transcript);
      if (
        transcript.toLowerCase().includes(t("next").toLowerCase()) ||
        transcript.toLowerCase().includes(t("net").toLowerCase())
      ) {
        handleNext();
      } else if (transcript.toLowerCase().includes(t("back").toLowerCase())) {
        handleBack();
      } else if (transcript.toLowerCase().includes(t("stop").toLowerCase())) {
        ExpoSpeechRecognitionModule.stop();
        setIsActiveVoiceCommand(false);
      }
    };

    handleVoiceCommands();
  }, [transcript]);

  return (
    <SafeAreaView style={{ flex: 1, backgroundColor: c(white.DEFAULT, black[100]) }}>
      <StatusBar backgroundColor={c(white.DEFAULT, black[100])} />
      <View className='size-full'>
        {isLoading || isRefetching ? (
          <View className='flex-center size-full'>
            <ActivityIndicator
              color={globalStyles.color.primary}
              size={"large"}
            />
          </View>
        ) : (
          <View className='flex-1'>
            <View className='absolute left-4 top-3 z-10'>
              <BackButton
                onPress={router.back}
                style={{
                  backgroundColor: c(white.DEFAULT, black.DEFAULT),
                  padding: 12,
                  borderRadius: 12,
                  shadowColor: "#000",
                  shadowOffset: { width: 0, height: 2 },
                  shadowOpacity: 0.25,
                  shadowRadius: 3.84,
                  elevation: 5
                }}
              />
            </View>

            {/* Header */}
            <View className='flex-center mt-10'>
              <View className='gap-4'>
                <View className='flex-center flex-row gap-2'>
                  <MaterialIcons
                    name='keyboard-voice'
                    size={22}
                    color={c(black.DEFAULT, white.DEFAULT)}
                  />
                  <Text className='base-semibold text-black_white'>
                    {t("voiceCommand")}
                  </Text>
                  <Switch
                    testID='active-voice-switch'
                    trackColor={{ false: `${inactiveTrackColor}`, true: `${primary}` }}
                    thumbColor={isActiveVoiceCommand ? `${thumbColor}` : `${thumbColor}`}
                    onValueChange={handleToggleVoiceCommand}
                    value={isActiveVoiceCommand}
                  />
                </View>

                <View className='flex-center flex-row gap-2'>
                  <MaterialCommunityIcons
                    name='account-tie-voice-outline'
                    size={22}
                    color={c(black.DEFAULT, white.DEFAULT)}
                  />
                  <Text className='base-semibold text-black_white'>{t("speaking")}</Text>
                  <Switch
                    testID='active-speaking-switch'
                    trackColor={{ false: `${inactiveTrackColor}`, true: `${primary}` }}
                    thumbColor={isActiveSpeaking ? `${thumbColor}` : `${thumbColor}`}
                    onValueChange={handleToggleSpeaking}
                    value={isActiveSpeaking}
                  />
                </View>
              </View>

              <Text className='h3-bold text-black_white my-8'>
                {t("step")} {currentStep}
              </Text>
            </View>

            {/* Carousel */}
            {sliderData !== undefined && sliderData !== null && sliderData.length > 0 && (
              <View>
                <Slider data={sliderData} />
              </View>
            )}

            {/* Step description */}
            <View className='flex-1 px-4'>
              {sliderData !== undefined &&
                sliderData !== null &&
                sliderData.length > 0 && (
                  <View className='my-8 h-[1px] w-full bg-primary'></View>
                )}
              <View className='gap-4'>
                <FlatList
                  data={sortedSteps}
                  keyExtractor={step => step.id}
                  contentContainerStyle={{ paddingBottom: 100 }}
                  renderItem={({ item }) => (
                    <View className='my-2'>
                      <Step
                        key={item.id}
                        isCookingMode
                        isActive={item.ordinalNumber === currentStep}
                        content={item.content}
                        ordinalNumber={item.ordinalNumber}
                        attachedImageUrls={item.attachedImageUrls}
                      />
                    </View>
                  )}
                />
              </View>
            </View>

            {/* Navigate buttons */}
            <View className='flex-center mb-4 flex-row gap-4 px-4'>
              <TouchableWithoutFeedback onPress={handleBack}>
                <View className='flex-center flex-1 rounded-3xl bg-gray-400 px-5 py-3'>
                  <Text className='body-semibold text-black'>{t("back")}</Text>
                </View>
              </TouchableWithoutFeedback>
              <TouchableWithoutFeedback onPress={handleNext}>
                <View className='flex-center flex-1 rounded-3xl bg-primary px-5 py-3'>
                  <Text className='body-semibold text-white_black'>{t("next")}</Text>
                </View>
              </TouchableWithoutFeedback>
            </View>
          </View>
        )}
      </View>
    </SafeAreaView>
  );
};

export default CookingMode;
