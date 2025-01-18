import { useRecipeSteps } from "@/api/recipe";
import { globalStyles } from "@/components/common/GlobalStyles";
import Slider from "@/components/screen/community/Slider";
import Step from "@/components/screen/community/Step";
import { useLocalSearchParams } from "expo-router";
import { useMemo, useState } from "react";
import { useTranslation } from "react-i18next";
import {
  ActivityIndicator,
  Text,
  TouchableWithoutFeedback,
  View,
  SafeAreaView
} from "react-native";
import { useSharedValue } from "react-native-reanimated";

const CookingMode = () => {
  const { t } = useTranslation("cookingMode");
  const { id } = useLocalSearchParams<{ id: string }>();
  const { data, isLoading, refetch, isRefetching } = useRecipeSteps(id);

  const [currentStep, setCurrentStep] = useState<number>(2);

  const sortedSteps = useMemo(() => {
    return data?.sort((a, b) => a.ordinalNumber - b.ordinalNumber);
  }, [data]);

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
            {/* Header */}
            <View></View>

            {/* Carousel */}
            <View className='flex-1'>
              {sliderData !== undefined && sliderData !== null && (
                <Slider data={sliderData} />
              )}
            </View>

            {/* Step description */}
            <View className='flex-1 px-4'>
              <Text>test</Text>
              <View className='my-8 h-[1px] w-full bg-primary'></View>
              <View className='gap-4'>
                {sortedSteps?.map(step => {
                  return (
                    <Step
                      key={step.id}
                      content={step.content}
                      ordinalNumber={step.ordinalNumber}
                      attachedImageUrls={step.attachedImageUrls}
                    />
                  );
                })}
              </View>
            </View>

            {/* Navigate buttons */}
            <View className='flex-center mb-4 flex-row gap-4 px-4'>
              <TouchableWithoutFeedback onPress={handleBack}>
                <View className='flex-center max-w-[152px] flex-1 rounded-3xl bg-gray-500 px-5 py-2'>
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
