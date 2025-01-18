import { useRecipeSteps } from "@/api/recipe";
import BackButton from "@/components/BackButton";
import { globalStyles } from "@/components/common/GlobalStyles";
import Slider from "@/components/screen/community/Slider";
import Step from "@/components/screen/community/Step";
import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { MaterialCommunityIcons, MaterialIcons } from "@expo/vector-icons";
import { router, useLocalSearchParams } from "expo-router";
import { useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import {
  ActivityIndicator,
  Text,
  TouchableWithoutFeedback,
  View,
  SafeAreaView,
  Switch,
  Platform,
  FlatList
} from "react-native";

const CookingMode = () => {
  const { t } = useTranslation("cookingMode");
  const { id } = useLocalSearchParams<{ id: string }>();
  const { data, isLoading, refetch, isRefetching } = useRecipeSteps(id);

  const [isActiveSpeaking, setIsActiveSpeaking] = useState(false);
  const [isActiveVoiceCommand, setIsActiveVoiceCommand] = useState(false);
  const [currentStep, setCurrentStep] = useState<number>(1);

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
    setIsActiveSpeaking(prev => !prev);
  };

  const handleToggleVoiceCommand = () => {
    setIsActiveVoiceCommand(prev => !prev);
  };

  const handleBack = () => {
    if (currentStep > 1) {
      setCurrentStep(prev => prev - 1);
    }
  };
  const handleNext = () => {
    if (currentStep < data?.length!) {
      setCurrentStep(prev => prev + 1);
    }
  };

  const sliderData =
    sortedSteps
      ?.find(item => item.ordinalNumber === currentStep)
      ?.attachedImageUrls?.filter(item => item) || [];

  return (
    <SafeAreaView style={{ flex: 1, backgroundColor: globalStyles.color.light }}>
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
                  backgroundColor: globalStyles.color.light,
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
                    size={18}
                    color='black'
                  />
                  <Text className='body-regular'>{t("voiceCommand")}</Text>
                  <Switch
                    trackColor={{ false: `${inactiveTrackColor}`, true: `${primary}` }}
                    thumbColor={isActiveSpeaking ? `${thumbColor}` : `${thumbColor}`}
                    onValueChange={handleToggleSpeaking}
                    value={isActiveSpeaking}
                  />
                </View>

                <View className='flex-center flex-row gap-2'>
                  <MaterialCommunityIcons
                    name='account-tie-voice-outline'
                    size={18}
                    color='black'
                  />
                  <Text className='body-regular'>{t("speaking")}</Text>
                  <Switch
                    trackColor={{ false: `${inactiveTrackColor}`, true: `${primary}` }}
                    thumbColor={isActiveVoiceCommand ? `${thumbColor}` : `${thumbColor}`}
                    onValueChange={handleToggleVoiceCommand}
                    value={isActiveVoiceCommand}
                  />
                </View>
              </View>

              <Text className='h3-bold my-8'>
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
                    <Step
                      key={item.id}
                      isCookingMode
                      isActive={item.ordinalNumber === currentStep}
                      content={item.content}
                      ordinalNumber={item.ordinalNumber}
                      attachedImageUrls={item.attachedImageUrls}
                    />
                  )}
                />
              </View>
            </View>

            {/* Navigate buttons */}
            <View className='flex-center mb-4 flex-row gap-4 px-4'>
              <TouchableWithoutFeedback onPress={handleBack}>
                <View className='flex-center max-w-[152px] flex-1 rounded-3xl bg-gray-400 px-5 py-2'>
                  <Text className='body-semibold text-black'>{t("back")}</Text>
                </View>
              </TouchableWithoutFeedback>
              <TouchableWithoutFeedback onPress={handleNext}>
                <View className='flex-center max-w-[152px] flex-1 rounded-3xl bg-primary px-5 py-2'>
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
