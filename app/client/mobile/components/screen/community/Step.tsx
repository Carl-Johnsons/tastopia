import { globalStyles } from "@/components/common/GlobalStyles";
import PreviewImage from "@/components/common/PreviewImage";
import useDarkMode from "@/hooks/useDarkMode";
import { Image } from "expo-image";
import React from "react";
import { StyleSheet, Text, View } from "react-native";

type StepProps = {
  isCookingMode?: boolean;
  isActive: boolean;
  content: string;
  ordinalNumber: number;
  attachedImageUrls: string[] | null;
};

const Step = ({
  isCookingMode = false,
  isActive = false,
  content,
  ordinalNumber,
  attachedImageUrls
}: StepProps) => {
  return (
    <View className={`${isCookingMode ? (isActive ? "opacity-1" : "opacity-60") : ""}`}>
      <View className='flex-row gap-3'>
        <View className='flex-center size-7 rounded-full bg-primary'>
          <Text className={`text-white_black subtitle-medium`}>{ordinalNumber}</Text>
        </View>
        <View className='w-full max-w-[90%] flex-col gap-2'>
          <Text className='text-black_white'>{content}</Text>
          <View className='flex-row gap-2'>
            {!isCookingMode &&
              attachedImageUrls !== null &&
              attachedImageUrls?.length > 0 &&
              attachedImageUrls.map((image, index) => {
                return (
                  <PreviewImage
                    key={image + index}
                    imgUrl={image}
                    className='h-[100px] flex-1 rounded-lg'
                  />
                );
              })}
          </View>
        </View>
      </View>
    </View>
  );
};

export default Step;

const styles = StyleSheet.create({});
