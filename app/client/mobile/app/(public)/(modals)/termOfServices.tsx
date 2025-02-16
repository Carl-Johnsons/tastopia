import { useTranslation } from "react-i18next";
import { ScrollView, Text, View } from "react-native";

const TermAndServices = () => {
  const { t } = useTranslation("termOfServices");

  return (
    <ScrollView className='bg-white_black font-primary p-4'>
      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section1.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section1.content1")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section1.content2")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section1.content3")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section1.content4")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section2.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section2.content1")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section2.list1")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section3.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section3.content1")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section3.content2")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section4.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section4.content1")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section4.content2")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section4.content3")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section4.content4")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section5.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section5.content1")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section5.content2")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section5.content3")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section5.list1")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section5.content4")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section6.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section6.content1")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section6.content2")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section6.content3")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section6.list1")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section7.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section7.content1")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section7.content2")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section8.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section8.content1")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section8.content2")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section9.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>{t("section9.content1")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section9.content2")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section9.content3")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section9.content4")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section9.content5")}</Text>
      <Text className='text-black_white font-primary mb-2'>{t("section9.content6")}</Text>
      <Text className='text-black_white font-primary mb-4'>{t("section9.content7")}</Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section10.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section10.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section10.content2")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section11.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section11.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section11.content2")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section11.list1")}</Text>
      </View>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section12.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section12.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section12.content2")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section12.content3")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section12.list1")}</Text>
      </View>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section13.title")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section13.list1")}</Text>
      </View>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section14.title")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section14.content1")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section15.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section15.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section15.content2")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section15.list1")}</Text>
      </View>
      <Text className='text-black_white font-primary mb-2'>
        {t("section15.content3")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section15.list2")}</Text>
      </View>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section16.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section16.content1")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section16.list1")}</Text>
      </View>
      <Text className='text-black_white font-primary mb-2'>
        {t("section16.content2")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section16.content3")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section16.content4")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section16.content5")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section16.list2")}</Text>
      </View>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section17.title")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section17.content1")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section18.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section18.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section18.content2")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section19.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section19.content1")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section19.list1")}</Text>
      </View>
      <Text className='text-black_white font-primary mb-4'>
        {t("section19.content2")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section20.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section20.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section20.content2")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section20.content3")}
      </Text>
      <View className='text-black_white font-primary mb-4'>
        <Text className='text-black_white font-primary mb-2'>{t("section20.list1")}</Text>
      </View>
      <Text className='text-black_white font-primary mb-4'>
        {t("section20.content4")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section21.title")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section21.content1")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section22.title")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section22.content1")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section23.title")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section23.content1")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section24.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section24.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section24.content2")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section24.content3")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section25.title")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section25.content1")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section26.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content2")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content3")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content4")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content5")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content6")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section26.content7")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section26.content8")}
      </Text>

      <Text className='text-black_white font-primary mb-2 font-bold text-lg'>
        {t("section27.title")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section27.content1")}
      </Text>
      <Text className='text-black_white font-primary mb-2'>
        {t("section27.content2")}
      </Text>
      <Text className='text-black_white font-primary mb-4'>
        {t("section27.content3")}
      </Text>
      <View className='h-4' />
    </ScrollView>
  );
};

export default TermAndServices;
