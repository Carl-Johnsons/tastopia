import SettingRecipe from "@/components/common/SettingRecipe";
import Content from "@/components/screen/history/Content";
import Header from "@/components/screen/history/Header";
import {
  HistoryProvider,
  useHistoryContext
} from "@/components/screen/history/HistoryProvider";
import { selectHistory } from "@/slices/history.slice";
import { SafeAreaView, View } from "react-native";

export default function History() {
  return (
    <HistoryProvider>
      <SafeAreaView>
        <View className='bg-white_black200 gap-3'>
          <Header />
          <Content />
        </View>
      </SafeAreaView>
      <RecipeBottomSheet />
    </HistoryProvider>
  );
}

const RecipeBottomSheet = () => {
  const { currentRecipeId, currentAuthorId } = selectHistory();
  const { bottomSheetRef } = useHistoryContext();

  return (
    <SettingRecipe
      id={currentRecipeId}
      authorId={currentAuthorId}
      ref={bottomSheetRef}
    />
  );
};
