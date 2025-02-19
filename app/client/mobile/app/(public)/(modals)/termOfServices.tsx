import { useTranslation } from "react-i18next";
import { ScrollView, Text, View } from "react-native";

const TermAndServices = () => {
  const { t } = useTranslation("termOfServices");

  return (
    <ScrollView className='text-black_white p-4 font-primary'>
      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section1.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section1.content1")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section1.content2")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section1.content3")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section1.content4")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section2.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section2.content1")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section2.list1")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section3.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section3.content1")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section3.content2")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section4.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section4.content1")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section4.content2")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section4.content3")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section4.content4")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section5.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section5.content1")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section5.content2")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section5.content3")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section5.list1")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section5.content4")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section6.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section6.content1")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section6.content2")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section6.content3")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section6.list1")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section7.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section7.content1")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section7.content2")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section8.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section8.content1")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section8.content2")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section9.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section9.content1")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section9.content2")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section9.content3")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section9.content4")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section9.content5")}</Text>
      <Text className='text-black_white mb-2 font-primary'>{t("section9.content6")}</Text>
      <Text className='text-black_white mb-4 font-primary'>{t("section9.content7")}</Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section10.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section10.content1")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section10.content2")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section11.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section11.content1")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section11.content2")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section11.list1")}</Text>
      </View>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section12.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section12.content1")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section12.content2")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section12.content3")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section12.list1")}</Text>
      </View>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section13.title")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section13.list1")}</Text>
      </View>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section14.title")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section14.content1")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section15.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section15.content1")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section15.content2")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section15.list1")}</Text>
      </View>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section15.content3")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section15.list2")}</Text>
      </View>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section16.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section16.content1")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section16.list1")}</Text>
      </View>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section16.content2")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section16.content3")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section16.content4")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section16.content5")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section16.list2")}</Text>
      </View>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section17.title")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section17.content1")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section18.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section18.content1")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section18.content2")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section19.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section19.content1")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section19.list1")}</Text>
      </View>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section19.content2")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section20.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section20.content1")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section20.content2")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section20.content3")}
      </Text>
      <View className='text-black_white mb-4 font-primary'>
        <Text className='text-black_white mb-2 font-primary'>{t("section20.list1")}</Text>
      </View>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section20.content4")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section21.title")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section21.content1")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section22.title")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section22.content1")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section23.title")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section23.content1")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section24.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section24.content1")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section24.content2")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section24.content3")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section25.title")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section25.content1")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section26.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content1")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content2")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content3")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content4")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content5")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content6")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section26.content7")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section26.content8")}
      </Text>

      <Text className='text-black_white mb-2 font-primary text-lg font-bold'>
        {t("section27.title")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section27.content1")}
      </Text>
      <Text className='text-black_white mb-2 font-primary'>
        {t("section27.content2")}
      </Text>
      <Text className='text-black_white mb-4 font-primary'>
        {t("section27.content3")}
      </Text>
      <View className='h-4' />
    </ScrollView>
  );
};

export default TermAndServices;
