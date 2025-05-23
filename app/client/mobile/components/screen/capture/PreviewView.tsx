import { addTagValue } from "@/slices/searchRecipe.slice";
import { Canvas, DiffRect, rect, rrect } from "@shopify/react-native-skia";
import { Dispatch, SetStateAction, useCallback, useState } from "react";
import { FontAwesome, Ionicons } from "@expo/vector-icons";
import { globalStyles } from "@/components/common/GlobalStyles";
import { useAppDispatch } from "@/store/hooks";
import { useBounce } from "@/hooks";
import { useRouter } from "expo-router";
import { useTranslation } from "react-i18next";
import Button from "@/components/Button";
import {
  View,
  Image,
  ActivityIndicator,
  Text,
  TouchableWithoutFeedback
} from "react-native";
import { selectLanguageSetting } from "@/slices/setting.slice";
import { SETTING_VALUE } from "@/constants/settings";

const RNFS = require("react-native-fs");

type Props = {
  photo: {
    uri: string;
    width: number;
    height: number;
  };
  setPhoto: Dispatch<
    SetStateAction<{
      uri: string;
      width: number;
      height: number;
    }>
  >;
  isSearching: boolean;
  setIsSearching: Dispatch<SetStateAction<boolean>>;
  prediction: IngredientStreamResponse | undefined;
  setPrediction: Dispatch<SetStateAction<IngredientStreamResponse | undefined>>;
  isPredictLoading: boolean;
  showBoundingBoxes?: boolean;
};

const PreviewView = ({
  photo,
  setPhoto,
  isSearching,
  setIsSearching,
  prediction,
  setPrediction,
  isPredictLoading,
  showBoundingBoxes
}: Props) => {
  const { t } = useTranslation("capture");

  const [imageContainerDimension, setImageContainerDimension] = useState({
    width: 0,
    height: 0
  });
  const dispatch = useAppDispatch();
  const language = selectLanguageSetting();

  const router = useRouter();
  const ingredientPredict =
    (language === SETTING_VALUE.LANGUAGE.VIETNAMESE
      ? prediction?.classifications?.[0]?.name?.vi
      : prediction?.classifications?.[0]?.name?.en) ?? "";
  const ingredientPredictCode = prediction?.classifications?.[0]?.code ?? "";

  // === Style ===
  const { animate: animatedSearchBtn, animatedStyles: animatedSearchBtnStyles } =
    useBounce();

  const handleBackInPreview = () => {
    RNFS.unlink(photo.uri);
    setPrediction(undefined);
    setPhoto({
      uri: "",
      width: 0,
      height: 0
    });
  };

  const handleSearch = useCallback(() => {
    if (isSearching) return;
    animatedSearchBtn();

    setIsSearching(true);
    dispatch(addTagValue({ code: ingredientPredictCode, value: ingredientPredict }));

    router.push({
      pathname: "/(protected)/search"
    });
  }, [ingredientPredict, isSearching]);

  return (
    <>
      <View className='w-full flex-1'>
        <View
          className='h-[80%] w-full'
          onLayout={event => {
            const { width, height } = event.nativeEvent.layout;
            setImageContainerDimension({
              width,
              height
            });
          }}
        >
          <Image
            source={{ uri: photo.uri }}
            style={{ flex: 1, resizeMode: "contain" }}
          />
        </View>
        {showBoundingBoxes && (
          <Canvas
            style={{
              position: "absolute",
              top: 0,
              left: 0,
              right: 0,
              bottom: 0
            }}
          >
            {(prediction?.boxes ?? []).map((box, index) => {
              const scaleFactor = Math.min(
                imageContainerDimension.width / photo.width,
                imageContainerDimension.height / photo.height
              );

              const displayWidth = photo.width * scaleFactor;
              const displayHeight = photo.height * scaleFactor;
              const offsetY = (imageContainerDimension.height - displayHeight) / 2;

              const x1 = box[0] * displayWidth;
              const y1 = offsetY + box[1] * displayHeight;
              const x2 = box[2] * displayWidth;
              const y2 = offsetY + box[3] * displayHeight;
              const boxWidth = x2 - x1;
              const boxHeight = y2 - y1;
              const line_width = 2;
              const outer = rrect(rect(x1, y1, boxWidth, boxHeight), 0, 0);
              const inner = rrect(
                rect(
                  x1 + line_width,
                  y1 + line_width,
                  boxWidth - line_width * 2,
                  boxHeight - line_width * 2
                ),
                0,
                0
              );
              return (
                <DiffRect
                  key={index}
                  outer={outer}
                  inner={inner}
                  color='red'
                />
              );
            })}
          </Canvas>
        )}
        <View className='flex-1 items-center justify-center gap-4'>
          {isPredictLoading ? (
            <>
              <ActivityIndicator
                size='large'
                color={globalStyles.color.primary}
              />
            </>
          ) : (
            <>
              {ingredientPredict !== "" ? (
                <>
                  <Text className='font-bold text-3xl text-primary'>
                    {ingredientPredict}
                  </Text>
                  <Button
                    onPress={handleSearch}
                    style={animatedSearchBtnStyles}
                    className='rounded-full bg-primary p-3'
                  >
                    <View className='flex-row items-center'>
                      <FontAwesome
                        name='search'
                        size={24}
                        color='black'
                        className='pr-4'
                      />
                      <Text className='font-bold text-xl'>{t("searchForRecipes")}</Text>
                    </View>
                  </Button>
                </>
              ) : (
                <>
                  <Text className='font-bold text-xl text-center text-primary'>
                    {t("invalidIngredient")}
                  </Text>
                  <Button
                    onPress={handleBackInPreview}
                    style={animatedSearchBtnStyles}
                    className='rounded-full bg-primary p-3'
                  >
                    <View className='flex-row items-center'>
                      <FontAwesome
                        name='search'
                        size={24}
                        color='black'
                        className='pr-4'
                      />
                      <Text className='font-bold text-xl'>{t("tryAgain")}</Text>
                    </View>
                  </Button>
                </>
              )}
            </>
          )}
        </View>
      </View>
      <View className='absolute left-0 right-0 top-0 w-full flex-1 flex-row justify-between pl-4 pr-4 pt-8'>
        <View>
          <TouchableWithoutFeedback onPress={handleBackInPreview}>
            <View>
              <Ionicons
                name='chevron-back'
                size={28}
                color='white'
                style={{
                  textShadowColor: "rgba(0, 0, 0, 0.75)",
                  textShadowOffset: { width: 1, height: 3 },
                  textShadowRadius: 1
                }}
              />
            </View>
          </TouchableWithoutFeedback>
        </View>
      </View>
    </>
  );
};

export { PreviewView };
